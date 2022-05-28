using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Invoicing.Core.Features.StockIn.Queries
{

    internal class WarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, Result<List<GetWarehouseResponse>>>
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<StockInQueryHandler> _localizer;
        private readonly IProductService _productService;

        public WarehouseQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<StockInQueryHandler> localizer,
            IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
        }

        public async Task<Result<List<GetWarehouseResponse>>> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _context.Warehouses.AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();


            var mappedData = _mapper.Map<List<Warehouse>, List<GetWarehouseResponse>>(warehouses);

            return await Result<List<GetWarehouseResponse>>.SuccessAsync(data: mappedData);
        }
    }
}