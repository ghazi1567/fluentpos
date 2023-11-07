using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Modules.Invoicing.Core.Features.Warehouses.Commands;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using System.Collections.Generic;
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
            CreateMap<WarehouseDto, Warehouse>().ReverseMap();

            CreateMap<InternalOrder, InternalOrderDto>()
                .ForMember(dest => dest.PaymentGatewayNames, opt => opt.MapFrom(src => new List<string> { src.PaymentGatewayNames }));

            // CreateMap<InternalCustomer, InternalCustomerDto>().ReverseMap();
            CreateMap<InternalAddress, InternalAddressDto>().ReverseMap();
            CreateMap<OrderFulfillment, OrderFulfillmentDto>().ReverseMap()
                  .ForMember(dest => dest.TrackingNumbers, opt => opt.MapFrom(src => string.Join(", ", src.TrackingNumbers.ToArray())))
                  .ForMember(dest => dest.TrackingUrls, opt => opt.MapFrom(src => string.Join(", ", src.TrackingUrls.ToArray())));
            CreateMap<OrderLineItem, OrderLineItemDto>().ReverseMap();

            CreateMap<ShopifySharp.Order, RegisterOrderCommand>(MemberList.Source)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalShippingPrice, opt => opt.MapFrom(src => src.TotalShippingPriceSet.ShopMoney.Amount))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<RegisterOrderCommand, InternalOrder>(MemberList.Source)
            .ForMember(dest => dest.PaymentGatewayNames, opt => opt.MapFrom(src => string.Join(", ", src.PaymentGatewayNames.ToArray())));
            CreateMap<ShopifySharp.Customer, InternalCustomerDto>(MemberList.Source)
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ShopifySharp.Address, InternalAddressDto>(MemberList.Source)
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .IgnoreAllNonExisting();

            CreateMap<ShopifySharp.Fulfillment, OrderFulfillmentDto>(MemberList.Source)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ShopifySharp.Fulfillment, OrderFulfillment>(MemberList.Source)
               .IgnoreAllNonExisting()
               .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ShopifySharp.LineItem, OrderLineItemDto>(MemberList.Source)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ShopifySharp.LineItem, OrderLineItem>(MemberList.Source)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));

            CreateMap<OrderFulfillment, OrderFulfillmentDto>(MemberList.Source)
                .ForMember(dest => dest.TrackingNumbers, opt => opt.MapFrom(src => new List<string> { src.TrackingNumbers }))
                .ForMember(dest => dest.TrackingUrls, opt => opt.MapFrom(src => new List<string> { src.TrackingUrls }));

            CreateMap<ShopifySharp.Location, RegisterLocationCommand>(MemberList.Source)
              .IgnoreAllNonExisting()
              .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<RegisterLocationCommand, Warehouse>().ReverseMap();

            CreateMap<ShopifySharp.FulfillmentOrder, InternalFulfillmentOrderDto>()
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}