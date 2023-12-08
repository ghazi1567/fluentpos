// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateProductCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LocaleName { get; set; }

        public long BrandId { get; set; }

        public long CategoryId { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public string ImageUrl { get; set; }

        public decimal Tax { get; set; }

        public string TaxMethod { get; set; }

        public string BarcodeSymbology { get; set; }

        public bool IsAlert { get; set; }

        public decimal AlertQuantity { get; set; }

        public string Detail { get; set; }

        public UploadRequest UploadRequest { get; set; }

        public string productCode { get; set; }

        public int quantity { get; set; } = 0;

        public string location { get; set; }

        public string location2 { get; set; }

        public decimal? discountFactor { get; set; } = 0;

        public int OpeningStock { get; set; }

        public long WarehouseId { get; set; }
    }
}