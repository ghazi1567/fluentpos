// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Dtos.Shopify;
using MediatR;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands
{
    public class RegisterWebhookEventCommand : WebhookEventDto, IRequest<Result<long>>
    {

    }
}