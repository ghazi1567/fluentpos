using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Features.Reports
{
    internal class StockOutReportHandler : IRequestHandler<StockOutReportQuery, Result<List<StockDto>>>
    {
        private readonly IInventoryDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<StockReportHandler> _localizer;
        private readonly IProductService _productService;

        public StockOutReportHandler(
            IInventoryDbContext context,
            IMapper mapper,
            IStringLocalizer<StockReportHandler> localizer,
            IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
        }

        public async Task<Result<List<StockDto>>> Handle(StockOutReportQuery request, CancellationToken cancellationToken)
        {
            var query = _context.StockTransactions.AsQueryable();
            query = query.Where(x => x.Type == OrderType.StockOut);

            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                var startDate = request.StartDate.Value.StartDate();
                var endDate = request.EndDate.Value.EndDate();
                query = query.Where(x => x.Timestamp >= startDate && x.Timestamp <= endDate);
            }
            else if (request.StartDate.HasValue)
            {
                var startDate = request.StartDate.Value.StartDate();
                query = query.Where(x => x.Timestamp == startDate);
            }

            if (!string.IsNullOrEmpty(request.ProductId))
            {
                var productId = Guid.Parse(request.ProductId);
                query = query.Where(x => x.ProductId == productId);
            }

            var result = query.Where(x => x.Type == OrderType.StockOut).GroupBy(c => c.ProductId).
                  Select(g => new StockDto
                  {
                     ProductId = g.Key,
                     AvailableQuantity = g.Sum(s => s.Quantity)
                  }).ToList();

            return await Result<List<StockDto>>.SuccessAsync(data: result);
        }
    }
}