using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.StockIn.Queries
{
    internal class WarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, Result<List<WarehouseDto>>>,
        IRequestHandler<GetWarehouseByNamesQuery, Result<List<GetWarehouseResponse>>>,
        IRequestHandler<GetWarehouseByIdsQuery, Result<List<GetWarehouseResponse>>>,
        IRequestHandler<GetDefaultWarehouseQuery, WarehouseDto>

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

        public async Task<Result<List<WarehouseDto>>> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _context.Warehouses.AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();


            var mappedData = _mapper.Map<List<Warehouse>, List<WarehouseDto>>(warehouses);

            return await Result<List<WarehouseDto>>.SuccessAsync(data: mappedData);
        }

        public async Task<Result<List<GetWarehouseResponse>>> Handle(GetWarehouseByNamesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Warehouse, GetWarehouseResponse>> expression = e => new GetWarehouseResponse(e.Id, e.ShopifyId, e.Name, e.Active, e.ParentId, e.Latitude, e.Longitude, e.Phone, e.Address1, e.City, e.Code, e.PickupAddress);

            var queryable = _context.Warehouses.AsQueryable();
            if (request.Names.Count > 0)
            {
                queryable = queryable.Where(x => request.Names.Contains(x.Name));
            }

            var warehouses = await queryable.AsNoTracking()
              .OrderBy(x => x.Name)
              .Select(expression)
              .ToListAsync();

            return await Result<List<GetWarehouseResponse>>.SuccessAsync(data: warehouses);
        }

        public async Task<WarehouseDto> Handle(GetDefaultWarehouseQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _context.Warehouses.AsNoTracking().FirstOrDefaultAsync(x => x.Default == true);
            return _mapper.Map<Warehouse, WarehouseDto>(warehouses);
        }

        public async Task<Result<List<GetWarehouseResponse>>> Handle(GetWarehouseByIdsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Warehouse, GetWarehouseResponse>> expression = e => new GetWarehouseResponse(e.Id, e.ShopifyId, e.Name, e.Active, e.ParentId, e.Latitude, e.Longitude, e.Phone, e.Address1, e.City, e.Code, e.PickupAddress);

            var queryable = _context.Warehouses.AsQueryable();
            if (request.Ids.Count > 0)
            {
                queryable = queryable.Where(x => request.Ids.Contains(x.Id));
            }

            var warehouses = await queryable.AsNoTracking()
              .OrderBy(x => x.Name)
              .Select(expression)
              .ToListAsync();

            return await Result<List<GetWarehouseResponse>>.SuccessAsync(data: warehouses);
        }
    }
}