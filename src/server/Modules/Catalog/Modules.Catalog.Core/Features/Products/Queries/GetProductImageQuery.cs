// --------------------------------------------------------------------------------------------------
// <copyright file="GetProductImageQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetProductImageQuery : IRequest<Result<string>>
    {
        public Guid? Id { get; }

        public GetProductImageQuery(Guid productId)
        {
            Id = productId;
        }
    }

    public class GetProductImageByIdsQuery : IRequest<Result<List<GetProductImageByIdResponse>>>
    {
        public List<long?> shopifyIds { get; }

        public GetProductImageByIdsQuery(List<long?> productId)
        {
            shopifyIds = productId;
        }
    }
}