// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    public class ApproveOrderCommand : IRequest<Result<string>>
    {
        public long Id { get; set; }

        public long ShopifyId { get; set; }

        public string Reason { get; set; }

        public long FulFillOrderId { get; set; }

    }
}