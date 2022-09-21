﻿// --------------------------------------------------------------------------------------------------
// <copyright file="ProductProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Catalogs.Products;

namespace FluentPOS.Modules.Accounting.Core.Mappings
{
    public class AccountingProfile : Profile
    {
        public AccountingProfile()
        {
            //CreateMap<RegisterProductCommand, Product>().ReverseMap();
            //CreateMap<UpdateProductCommand, Product>().ReverseMap();
            //CreateMap<GetByIdCacheableFilter<Guid, Product>, GetProductByIdQuery>();
            //CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            //CreateMap<Product, GetProductsResponse>()
            //    .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name))
            //    .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name));
            //CreateMap<PaginatedProductFilter, GetProductsQuery>()
            //    .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}