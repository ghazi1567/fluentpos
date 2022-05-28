// --------------------------------------------------------------------------------------------------
// <copyright file="ProductProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using FluentPOS.Shared.DTOs.Sales.Orders;

namespace FluentPOS.Modules.Invoicing.Core.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<POProduct, POProductResponse>().ReverseMap();
            CreateMap<Product, StockInProductResponse>().ReverseMap();

        }
    }
}