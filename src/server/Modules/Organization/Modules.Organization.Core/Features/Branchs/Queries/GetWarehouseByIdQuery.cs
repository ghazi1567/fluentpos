// --------------------------------------------------------------------------------------------------
// <copyright file="GetBrandByIdQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Organizations.Branchs;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Catalog.Core.Features.Stores.Queries
{
    public class GetWarehouseByIdQuery : IRequest<Result<List<GetWarehouseByIdResponse>>>
    {
        public Guid Id { get; set; }

        public GetWarehouseByIdQuery(Guid id)
        {
                Id = id;
        }
    }
}