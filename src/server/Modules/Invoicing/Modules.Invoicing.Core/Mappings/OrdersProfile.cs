using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Sales.Orders;
using System.Linq;

namespace FluentPOS.Modules.Invoicing.Core.Mappings
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<GetSalesResponse, InternalOrder>().ReverseMap();
            CreateMap<GetOrderByIdResponse, InternalOrder>().ReverseMap();
            CreateMap<GetStockInByIdResponse, InternalOrder>().ReverseMap();
            CreateMap<PaginatedSalesFilter, GetSalesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<VarianceProductReportDto, VarianceProductReport>().ReverseMap();
            CreateMap<GetWarehouseResponse, Warehouse>().ReverseMap();

            CreateMap<InternalOrder, InternalOrderDto>().ReverseMap()
                .ForMember(dest => dest.PaymentGatewayNames, opt => opt.MapFrom(src => string.Join(", ", src.PaymentGatewayNames.ToArray())));


            //CreateMap<InternalCustomer, InternalCustomerDto>().ReverseMap();
            CreateMap<InternalAddress, InternalAddressDto>().ReverseMap();
            CreateMap<OrderFulfillment, OrderFulfillmentDto>().ReverseMap()
                  .ForMember(dest => dest.TrackingNumbers, opt => opt.MapFrom(src => string.Join(", ", src.TrackingNumbers.ToArray())))
                  .ForMember(dest => dest.TrackingUrls, opt => opt.MapFrom(src => string.Join(", ", src.TrackingUrls.ToArray())));
            CreateMap<OrderLineItem, OrderLineItemDto>().ReverseMap();

            CreateMap<ShopifySharp.Order, RegisterOrderCommand>(MemberList.Source)
                .IgnoreAllNonExisting()
             .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<ShopifySharp.Customer, InternalCustomerDto>(MemberList.Source)
            .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ShopifySharp.Address, InternalAddressDto>(MemberList.Source)
            .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .IgnoreAllNonExisting();

            CreateMap<ShopifySharp.Fulfillment, OrderFulfillmentDto>(MemberList.Source)
            .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .IgnoreAllNonExisting();

            CreateMap<ShopifySharp.LineItem, OrderLineItemDto>(MemberList.Source)
            .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .IgnoreAllNonExisting();
        }
    }
}