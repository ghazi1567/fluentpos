// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    public class ConfirmOrderCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }

        public long ShopifyId { get; set; }

        public List<OrderLineItemDto> LineItems { get; set; }

        public long? FulfillmentOrderId { get; set; }
    }
}