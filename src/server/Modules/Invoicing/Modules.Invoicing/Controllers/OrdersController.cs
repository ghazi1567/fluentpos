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
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal sealed class OrdersController : BaseController
    {

        [HttpGet]
        [Authorize(Policy = Permissions.Orders.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetOrdersQuery request)
        {
            var sales = await Mediator.Send(request);
            return Ok(sales);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { Id = id }));
        }

        [HttpGet("fo/{id}")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetFulfillmentOrderByIdAsync(long id, [FromQuery] long[] warehouseIds)
        {
            return Ok(await Mediator.Send(new GetFOByIdQuery { Id = id, WarehouseIds = warehouseIds }));
        }

        [HttpGet("GetByOrderNo/{OrderNo}")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetByOrderNoAsync(string OrderNo)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { OrderNo = $"#{OrderNo}" }));
        }

        [HttpGet("GetOrderForConfirm/{OrderNo}")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetOrderForConfirmAsync(string OrderNo, [FromQuery] long[] warehouseIds)
        {
            return Ok(await Mediator.Send(new GetOrderForConfirmQuery { OrderNo = $"#{OrderNo}", WarehouseIds = warehouseIds }));
        }

        [HttpGet("GetByTrackingNumber/{trackingNumber}")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetByTrackingNumber(string trackingNumber)
        {
            return Ok(await Mediator.Send(new GetOrderByTrackingNumberQuery { TrackingNumber = trackingNumber }));
        }

        [HttpGet("GetCityCorrectionOrder")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetCityCorrectionOrderAsync()
        {
            return Ok(await Mediator.Send(new GetCityCorrectionOrderQuery()));
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Orders.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterSaleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ApproveOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> CancelOrderAsync(ApproveOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CancelOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> CancelOrderAsync(CancelledOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("FulFillOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> FulFillOrderAsync(FulFillOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("MoveLocation")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> MoveLocationAsync(MoveLocationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ConfirmOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> ConfirmOrderAsync(ConfirmOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ReturnOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> ReturnOrderAsync(ReturnOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("AcceptOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> AcceptOrderAsync(AcceptOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("RejectOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> RejectOrderAsync(RejectOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ReQueueOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> ReQueueOrderAsync(ReQueueOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ScanLoadSheetOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> ScanLoadSheetOrderAsync(ScanLoadSheetOrderQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GenerateLoadSheet")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> GenerateLoadSheetOrderAsync(RegisterloadsheetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("ReGenerateLoadSheet")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> ReGenerateLoadSheetAsync(ReGenerateloadsheetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetLoadsheetById/{id}")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetLoadsheetByIdAsync(long id)
        {
            return Ok(await Mediator.Send(new GetLoadsheetInByIdQuery { Id = id }));
        }

        [HttpGet("GetLoadsheets")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetLoadsheetsAsync()
        {
            return Ok(await Mediator.Send(new GetLoadsheetsQuery()));
        }

        [HttpPost("DeliveredOrder")]
        [Authorize(Policy = Permissions.Orders.Update)]
        public async Task<IActionResult> DeliveredOrderAsync(UpdateStatusToDeliveredCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}