using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Sales.Orders;

namespace FluentPOS.Modules.Invoicing.Core.Mappings
{
     public class POProfile : Profile
    {
        public POProfile()
        {
            CreateMap<GetPurchaseOrderResponse, PurchaseOrder>().ReverseMap();
            CreateMap<GetPurchaseOrderByIdResponse, PurchaseOrder>().ReverseMap();
            CreateMap<PaginatedSalesFilter, GetPurchaseOrderQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));

        }
    }
}