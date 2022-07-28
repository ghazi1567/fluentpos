using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    internal class SalesQueryHandler :
                IRequestHandler<GetSalesQuery, PaginatedResult<GetSalesResponse>>,
                IRequestHandler<GetOrderByIdQuery, Result<GetOrderByIdResponse>>
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalesQueryHandler> _localizer;

        public SalesQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<SalesQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetSalesResponse>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Orders.AsNoTracking()
                .ProjectTo<GetSalesResponse>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.TimeStamp)
                .AsQueryable();

            // string ordering = new OrderByConverter().Convert(request.OrderBy);
            // queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.TimeStamp);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.ReferenceNumber.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%") || x.Note.Contains(request.SearchString));
            }

            if (request.OrderType != null)
            {
                queryable = queryable.Where(x => x.OrderType == request.OrderType);
            }

            if (request.Status != null)
            {
                queryable = queryable.Where(x => x.Status == request.Status);
            }

            var saleList = await queryable
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (saleList == null)
            {
                throw new SalesException(_localizer["Sales Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetSalesResponse>>(saleList);

        }

        public async Task<Result<GetOrderByIdResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.AsNoTracking()
                .Include(x => x.Products)
                .OrderBy(x => x.TimeStamp)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
            {
                throw new SalesException(_localizer["Order Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedData = _mapper.Map<Order, GetOrderByIdResponse>(order);

            return await Result<GetOrderByIdResponse>.SuccessAsync(data: mappedData);

        }
    }
}