using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetOrdersQuery : IRequest<PaginatedResult<InternalOrderDto>>
    {
        public int PageNumber { get;  set; }

        public int PageSize { get;  set; }

        public string[] OrderBy { get;  set; }

        public string SearchString { get;  set; }

        public OrderStatus? Status { get;  set; }

        public OrderType? OrderType { get;  set; }
    }
}