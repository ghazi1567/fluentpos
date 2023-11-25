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

        [HttpGet("fo/{id}")]
        [Authorize(Policy = Permissions.Sales.View)]
        public async Task<IActionResult> GetFulfillmentOrderByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetFOByIdQuery { Id = id }));
        }

        [HttpGet("GetByOrderNo/{OrderNo}")]
        [Authorize(Policy = Permissions.Sales.View)]
        public async Task<IActionResult> GetByOrderNoAsync(string OrderNo)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { OrderNo = $"#{OrderNo}" }));
        }

        [HttpGet("GetOrderForConfirm/{OrderNo}")]
        [Authorize(Policy = Permissions.Sales.View)]
        public async Task<IActionResult> GetOrderForConfirmAsync(string OrderNo)
        {
            return Ok(await Mediator.Send(new GetOrderForConfirmQuery { OrderNo = $"#{OrderNo}" }));
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

        [HttpPost("ConfirmOrder")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> ConfirmOrderAsync(ConfirmOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("AcceptOrder")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> AcceptOrderAsync(AcceptOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("RejectOrder")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> RejectOrderAsync(RejectOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ReQueueOrder")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> ReQueueOrderAsync(ReQueueOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ScanLoadSheetOrder")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> ScanLoadSheetOrderAsync(ScanLoadSheetOrderQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GenerateLoadSheet")]
        [Authorize(Policy = Permissions.Sales.Update)]
        public async Task<IActionResult> GenerateLoadSheetOrderAsync(RegisterloadsheetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetLoadsheetInBy/{id}")]
        [Authorize(Policy = Permissions.Sales.View)]
        public async Task<IActionResult> GetLoadsheetInByAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetLoadsheetInByIdQuery { Id = id }));
        }
    }
}