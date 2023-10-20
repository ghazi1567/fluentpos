using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Sales.Orders;

namespace FluentPOS.Modules.Invoicing.Core.Mappings
{
     public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<GetSalesResponse, InternalOrder>().ReverseMap();
            CreateMap<GetOrderByIdResponse, InternalOrder>().ReverseMap();
            CreateMap<GetStockInByIdResponse, InternalOrder>().ReverseMap();
            CreateMap<PaginatedSalesFilter, GetSalesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<VarianceProductReportDto, VarianceProductReport>().ReverseMap();
            CreateMap<GetWarehouseResponse, Warehouse>().ReverseMap();

        }
    }
}