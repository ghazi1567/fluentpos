// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands
{
    public class CloseInvoiceCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }

        public string Comments { get; set; }
    }
}