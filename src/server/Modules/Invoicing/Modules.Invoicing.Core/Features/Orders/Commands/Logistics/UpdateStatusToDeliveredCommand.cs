// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    public class UpdateStatusToDeliveredCommand : IRequest<Result<bool>>
    {
        public List<UpdateStatusToDelivered> Orders { get; set; }

    }

    public class UpdateStatusToDelivered
    {
        public long OrderId { get; set; }

        public long FulfillmentOrderId { get; set; }

        public string TrackingNumber { get; set; }
    }
}