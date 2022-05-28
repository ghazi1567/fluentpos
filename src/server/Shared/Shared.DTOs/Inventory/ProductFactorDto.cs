// --------------------------------------------------------------------------------------------------
// <copyright file="ProductFactorDto.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Inventory
{
    public class ProductFactorDto
    {

        public Guid ProductId { get; set; }

        public string Barcode { get; set; }

        public DateTime FactorDate { get; set; }

        public decimal FactorAmount { get; set; }
    }
}