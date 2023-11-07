// --------------------------------------------------------------------------------------------------
// <copyright file="OrdersController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal sealed class OrdersController : BaseController
    {

        [HttpGet]
        [Authorize(Policy = Permissions.Sales.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetOrdersQuery request)
        {
            var sales = await Mediator.Send(request);
            return Ok(sales);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Sales.View)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Sales.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterSaleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ApproveOrder")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> CancelOrderAsync(ApproveOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CancelOrder")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> CancelOrderAsync(CancelledOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("FulFillOrder")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> FulFillOrderAsync(FulFillOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("MoveLocation")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> MoveLocationAsync(MoveLocationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}