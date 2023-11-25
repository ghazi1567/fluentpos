using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Modules.Inventory.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class LevelsCommandHandler : IRequestHandler<ImportLevelCommand, Result<Guid>>
    {
        private readonly IStringLocalizer<LevelsCommandHandler> _localizer;
        private readonly IMapper _mapper;
        private readonly IInventoryDbContext _context;
        private readonly IShopifyInventoryService _shopifyInventoryService;
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;
        private readonly IStockService _stockService;

        public LevelsCommandHandler(
            IStringLocalizer<LevelsCommandHandler> localizer,
            IMapper mapper,
            IInventoryDbContext context,
            IShopifyInventoryService shopifyInventoryService,
            IProductService productService,
            IWarehouseService warehouseService,
            IStockService stockService)
        {
            _localizer = localizer;
            _mapper = mapper;
            _context = context;
            _shopifyInventoryService = shopifyInventoryService;
            _productService = productService;
            _warehouseService = warehouseService;
            _stockService = stockService;
        }

        public async Task<Result<Guid>> Handle(ImportLevelCommand request, CancellationToken cancellationToken)
        {

            var importFile = _mapper.Map<ImportFile>(request.ImportFile);
            importFile.CreatedAt = DateTimeOffset.Now;
            importFile.Status = "Pending";
            await _context.ImportFiles.AddAsync(importFile);
            await _context.SaveChangesAsync();

            var inventoryItemSKUs = importFile.ImportRecords.Select(x => x.SKU).Distinct().ToList();
            var inventoryItems = await _productService.GetProductBySKUs(inventoryItemSKUs);
            var locations = await _warehouseService.GetWarehouse(new List<string>());

            string status = "Completed";
            string note = string.Empty;

            List<InternalInventoryLevelDto> inventoryLevels = new List<InternalInventoryLevelDto>();

            foreach (var item in importFile.ImportRecords)
            {
                var inventoryItem = inventoryItems.Data.Where(x => x.SKU == item.SKU).FirstOrDefault();
                var location = locations.Data.Where(x => x.Name.Trim() == item.Warehouse).FirstOrDefault();

                try
                {
                    if (location != null && inventoryItem != null)
                    {
                        long? locationId = location.ShopifyId;

                        if (!locationId.HasValue)
                        {
                            var parentLocation = locations.Data.FirstOrDefault(x => x.Id == location.ParentId);
                            if (parentLocation != null)
                            {
                                locationId = parentLocation.ShopifyId;
                            }
                        }

                        var response = await _stockService.RecordTransaction(new Shared.DTOs.Inventory.StockTransactionDto
                        {
                            IgnoreRackCheck = item.IgnoreRackCheck,
                            inventoryItemId = inventoryItem.InventoryItemId.Value,
                            productId = inventoryItem.ProductId1,
                            quantity = item.Qty,
                            Rack = item.Rack,
                            type = Shared.DTOs.Sales.Enums.OrderType.StockUpdate,
                            warehouseId = location.Id,
                            SKU = item.SKU,
                            VariantId = item.ShopifyId
                        });

                        if (response.Succeeded)
                        {
                            inventoryLevels.Add(new InternalInventoryLevelDto
                            {
                                InventoryItemId = inventoryItem.InventoryItemId,
                                LocationId = locationId,
                                Available = item.Qty,
                                WarehouseId = location.Id,
                                ParentId = location.ParentId,
                            });

                            item.Status = "Completed";
                            _context.ImportRecords.Update(item);
                        }
                        else if (!response.Succeeded && response.Messages.Contains("Exists in other rack."))
                        {
                            status = "Completed with error";
                            item.Note = $"SKU# {inventoryItem.SKU} already exists in other rack of location: {location.Name}.";
                            note = $"{note} {item.Note} {Environment.NewLine}";
                            item.Status = "Failed";
                            _context.ImportRecords.Update(item);
                        }

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        status = "Completed with error";
                        note = $"{note} - Location or InventoryItem not found for SKU {item.SKU}. {Environment.NewLine}";
                    }
                }
                catch (Exception ex)
                {
                    status = "Completed with error";
                    item.Status = "Failed";
                    item.Note = ex.Message;
                    note = $"{note} {item.SKU} - {ex.Message} {Environment.NewLine}";
                    _context.ImportRecords.Update(item);
                    await _context.SaveChangesAsync();
                }
            }

            var savedImportFile = _context.ImportFiles.FirstOrDefault(x => x.Id == importFile.Id);
            savedImportFile.Status = status;
            savedImportFile.Note = note;

            var result = inventoryLevels
            .GroupBy(inv => new { inv.LocationId, inv.InventoryItemId })
            .Select(group => new InternalInventoryLevel
            {
                LocationId = group.Key.LocationId,
                InventoryItemId = group.Key.InventoryItemId,
                Available = group.Sum(inv => inv.Available)
            })
            .ToList();

            foreach (var inventoryItem in result)
            {
                var inventoryLevel = await _shopifyInventoryService.SetLevel(inventoryItem.InventoryItemId, inventoryItem.LocationId, inventoryItem.Available);
                var internalInventoryLevel = _mapper.Map<InternalInventoryLevel>(inventoryLevel);
                await _context.InventoryLevels.AddAsync(internalInventoryLevel);
            }

            _context.ImportFiles.Update(savedImportFile);
            await _context.SaveChangesAsync();

            string returnMessage = string.Format(_localizer["{0} Inventory Records Imported."], importFile.ImportRecords.Count());
            if (status == "Completed with error")
            {
                returnMessage = note;
            }

            return await Result<Guid>.SuccessAsync(importFile.Id, returnMessage);
        }


        private async Task UpdateShopifyInventoryLevels(ImportRecord item, List<GetProductVariantResponse> inventoryItems, List<GetWarehouseResponse> locations)
        {
            var inventoryItem = inventoryItems.Where(x => x.SKU == item.SKU).FirstOrDefault();
            var location = locations.Where(x => x.Name == item.Location).FirstOrDefault();
            ShopifySharp.InventoryLevel currentInventoryLevel = null;
            try
            {
                if (location != null && inventoryItem != null)
                {
                    // TODO
                    // var errorMessage = $"SKU# {inventoryItem.SKU} already exists in other rack of location: {location.Name}.";
                    currentInventoryLevel = await _shopifyInventoryService.ListAsync(inventoryItem.InventoryItemId.Value, location.ShopifyId.Value);
                    var inventoryLevel = await _shopifyInventoryService.SetLevel(inventoryItem.InventoryItemId, location.ShopifyId, item.Qty);
                    var internalInventoryLevel = _mapper.Map<InternalInventoryLevel>(inventoryLevel);

                    item.Status = "Completed";
                    _context.ImportRecords.Update(item);
                    await _context.InventoryLevels.AddAsync(internalInventoryLevel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //status = "Completed with error";
                    //note = $"{note} {item.SKU} - Location or InventoryItem missing. {Environment.NewLine}";
                }
            }
            catch (Exception ex)
            {
                //status = "Completed with error";
                item.Status = "Failed";
                item.Note = ex.Message;
                //note = $"{note} {item.SKU} - {ex.Message} {Environment.NewLine}";
                _context.ImportRecords.Update(item);
                await _context.SaveChangesAsync();
                if (currentInventoryLevel != null)
                {
                    var inventoryLevel = await _shopifyInventoryService.SetLevel(currentInventoryLevel.InventoryItemId, currentInventoryLevel.LocationId, int.Parse(currentInventoryLevel.Available.ToString()));
                }
                else
                {
                    var inventoryLevel = await _shopifyInventoryService.SetLevel(inventoryItem.InventoryItemId, location.ShopifyId, 0);
                }
            }
        }
    }
}