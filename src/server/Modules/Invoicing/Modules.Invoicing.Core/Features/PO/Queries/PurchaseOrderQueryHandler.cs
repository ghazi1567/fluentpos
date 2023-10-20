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
using FluentPOS.Modules.Invoicing.Core.Enums;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    internal class PurchaseOrderQueryHandler :
                IRequestHandler<GetPurchaseOrderQuery, PaginatedResult<GetPurchaseOrderResponse>>,
                IRequestHandler<GetPurchaseOrderByIdQuery, Result<GetPurchaseOrderByIdResponse>>
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalesQueryHandler> _localizer;

        public PurchaseOrderQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<SalesQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetPurchaseOrderResponse>> Handle(GetPurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.PurchaseOrders.AsNoTracking()
                .ProjectTo<GetPurchaseOrderResponse>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.TimeStamp)
                .AsQueryable();

            // string ordering = new OrderByConverter().Convert(request.OrderBy);
            // queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.TimeStamp);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.ReferenceNumber.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%")
                 || EF.Functions.Like(x.Note.ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            if (!string.IsNullOrEmpty(request.IsApproved))
            {
                queryable = queryable.Where(x => x.IsApproved == bool.Parse(request.IsApproved));
            }

            if (request.Status != null)
            {
                queryable = queryable.Where(x => x.Status == OrderStatus.Pending);
            }

            var saleList = await queryable
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (saleList == null)
            {
                throw new SalesException(_localizer["Sales Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetPurchaseOrderResponse>>(saleList);

        }

        public async Task<Result<GetPurchaseOrderByIdResponse>> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.PurchaseOrders.AsNoTracking()
               .Include(x => x.Products)
               .OrderBy(x => x.TimeStamp)
               .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
            {
                throw new SalesException(_localizer["Order Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedData = _mapper.Map<PurchaseOrder, GetPurchaseOrderByIdResponse>(order);

            return await Result<GetPurchaseOrderByIdResponse>.SuccessAsync(data: mappedData);
        }
    }
}