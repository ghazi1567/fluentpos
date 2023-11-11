// --------------------------------------------------------------------------------------------------
// <copyright file="ProductProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Features.Products.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Products.Queries;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using System;

namespace FluentPOS.Modules.Catalog.Core.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<RegisterProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Product>, GetProductByIdQuery>();
            CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            CreateMap<Product, GetProductsResponse>();
            CreateMap<PaginatedProductFilter, GetProductsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<ShopifySharp.Product, Product>()
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id));
            CreateMap<ShopifySharp.Product, RegisterProductCommand>(MemberList.Source)
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ShopifySharp.ProductVariant, ProductVariant>(MemberList.Source)
                .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.ShopifyProductId, opt => opt.MapFrom(src => src.ProductId))
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.ProductId, opt => opt.Ignore());

            CreateMap<ShopifySharp.ProductImage, ProductImage>(MemberList.Source)
              .ForMember(dest => dest.ShopifyId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}