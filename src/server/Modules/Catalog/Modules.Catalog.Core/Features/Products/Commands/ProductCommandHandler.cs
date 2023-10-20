// --------------------------------------------------------------------------------------------------
// <copyright file="ProductCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Features.Products.Events;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    internal class ProductCommandHandler :
        IRequestHandler<RegisterProductCommand, Result<Guid>>,
        IRequestHandler<RemoveProductCommand, Result<Guid>>,
        IRequestHandler<UpdateProductCommand, Result<Guid>>,
        IRequestHandler<ImportProductCommand, Result<Guid>>,
        IRequestHandler<UpdateFactorCommand, Result<Guid>>

    {
        private readonly IDistributedCache _cache;
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<ProductCommandHandler> _localizer;
        private readonly IStockService _stockService;
        private readonly IEntityReferenceService _referenceService;

        public ProductCommandHandler(
            ICatalogDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<ProductCommandHandler> localizer,
            IDistributedCache cache,
            IStockService stockService,
            IEntityReferenceService referenceService)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
            _stockService = stockService;
            _referenceService = referenceService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterProductCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (command.ShopifyId != null && await _context.Products.AnyAsync(p => p.ShopifyId == command.ShopifyId, cancellationToken))
            {
                return Result<Guid>.ReturnError(_localizer[$"Shopify Product with id: {command.ShopifyId} already exists."]);
            }

            var product = _mapper.Map<Product>(command);
            product.ReferenceNumber = await _referenceService.TrackAsync(product.GetType().Name);

            // var uploadRequest = command.UploadRequest;
            // if (uploadRequest != null)
            // {
            //     uploadRequest.FileName = $"P-{command.BarcodeSymbology}.{uploadRequest.Extension}";
            //     product.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
            // }

            // product.AddDomainEvent(new ProductRegisteredEvent(product));
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // if (product.OpeningStock > 0 && product.WarehouseId != Guid.Empty)
            // {
            //     await _stockService.RecordOpeningTransaction(product.Id, product.OpeningStock, product.ReferenceNumber, product.discountFactor, product.Cost, DateTime.Now, product.WarehouseId);
            // }

            return await Result<Guid>.SuccessAsync(Guid.Empty, _localizer["Product Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            //if (await _context.Products.Where(p => p.Id != command.Id).AnyAsync(p => p.BarcodeSymbology == command.BarcodeSymbology, cancellationToken))
            //{
            //    throw new CatalogException(_localizer["Barcode already exists."], HttpStatusCode.BadRequest);
            //}

            //var product = await _context.Products.Where(p => p.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            //if (product != null)
            //{
            //    product = _mapper.Map<Product>(command);
            //    var uploadRequest = command.UploadRequest;
            //    if (uploadRequest != null)
            //    {
            //        uploadRequest.FileName = $"P-{command.BarcodeSymbology}.{uploadRequest.Extension}";
            //        product.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
            //    }

            //    product.AddDomainEvent(new ProductUpdatedEvent(product));
            //    _context.Products.Update(product);
            //    await _context.SaveChangesAsync(cancellationToken);

            //    if (product.OpeningStock > 0 && product.WarehouseId != Guid.Empty)
            //    {
            //        await _stockService.RecordOpeningTransaction(product.Id, product.OpeningStock, product.ReferenceNumber, product.discountFactor, product.Cost, DateTime.Now, product.WarehouseId);
            //    }

            //    await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Product>(command.Id), cancellationToken);
            //    return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Updated"]);
            //}
            //else
            //{
            //    throw new CatalogException(_localizer["Product Not Found!"], HttpStatusCode.NotFound);
            //}
            return await Result<Guid>.SuccessAsync(Guid.Empty, _localizer["Product Updated"]);

        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            //var product = await _context.Products.Where(p => p.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            //if (product != null)
            //{
            //    product.AddDomainEvent(new ProductRemovedEvent(product.Id));
            //    _context.Products.Remove(product);
            //    await _context.SaveChangesAsync(cancellationToken);
            //    await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Product>(command.Id), cancellationToken);

            //    if (product.OpeningStock > 0 && product.WarehouseId != Guid.Empty)
            //    {
            //        await _stockService.RecordOpeningTransaction(product.Id, 0, product.ReferenceNumber, product.discountFactor, product.Cost, DateTime.Now, product.WarehouseId);
            //    }

            //    return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Deleted"]);
            //}
            //else
            //{
            //    throw new CatalogException(_localizer["Product Not Found!"], HttpStatusCode.NotFound);
            //}
            return await Result<Guid>.SuccessAsync(Guid.Empty, _localizer["Product Deleted"]);

        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(ImportProductCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            //Guid productId = Guid.Empty;
            //if (command.products.Count > 0)
            //{
            //    var barcodes = command.products.Select(x => x.BarcodeSymbology).Distinct();
            //    var existingProducts = await _context.Products.Where(p => barcodes.Contains(p.BarcodeSymbology)).ToListAsync(cancellationToken);
            //    var importProducts = _mapper.Map<List<Product>>(command.products);

            //    var differenceQuery = importProducts.Where(x => !existingProducts.Any(z => z.BarcodeSymbology == x.BarcodeSymbology)).ToList();
            //    if (differenceQuery.Count > 0)
            //    {
            //        _context.OperationName = "ProductImport";
            //        _context.Products.AddRange(differenceQuery);
            //        _context.SaveChanges();

            //        foreach (var product in differenceQuery)
            //        {
            //            if (product.OpeningStock > 0 && product.WarehouseId != Guid.Empty)
            //            {
            //                await _stockService.RecordOpeningTransaction(product.Id, product.OpeningStock, product.Id.ToString(), product.discountFactor, product.Cost, DateTime.Now, product.WarehouseId);
            //            }
            //        }
            //    }
            //}

            return await Result<Guid>.SuccessAsync(Guid.Empty, _localizer["Product Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateFactorCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            //Guid productId = Guid.Empty;

            //foreach (var item in command.Products)
            //{
            //    var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == item.ProductId);
            //    if (product != null)
            //    {
            //        product.discountFactor = item.FactorAmount;
            //        product.FactorUpdateOn = DateTime.Now;
            //        _context.Products.Update(product);
            //    }
            //}
            //await _context.SaveChangesAsync(cancellationToken);

            //await _stockService.UpdateFactor(command.Products, command.updateFrom);
            return await Result<Guid>.SuccessAsync(Guid.Empty, _localizer["Product Saved"]);
        }


    }
}