// --------------------------------------------------------------------------------------------------
// <copyright file="ProductsController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Features.Products.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Products.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Catalog.Controllers
{
    [ApiVersion("1")]
    internal sealed class ProductsController : BaseController
    {
        [HttpPost("GetAll")]
        [Authorize(Policy = Permissions.Products.ViewAll)]
        public async Task<IActionResult> GetAllAsync(PaginatedProductFilter filter)
        {
            var request = Mapper.Map<GetProductsQuery>(filter);
            var products = await Mediator.Send(request);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Products.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<long, Product> filter)
        {
            var request = Mapper.Map<GetProductByIdQuery>(filter);
            var product = await Mediator.Send(request);
            return Ok(product);
        }

        [HttpGet("image/{id}")]
        [Authorize(Policy = Permissions.Products.View)]
        public async Task<IActionResult> GetImageByIdAsync(long id)
        {
            return Ok(await Mediator.Send(new GetProductImageQuery(id)));
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Products.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Products.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Products.Remove)]
        public async Task<IActionResult> RemoveAsync(long id)
        {
            return Ok(await Mediator.Send(new RemoveProductCommand(id)));
        }

        [HttpPost("Import")]
        [Authorize(Policy = Permissions.Products.Register)]
        public async Task<IActionResult> ImportRegisterAsync(ImportProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("updatePromotion")]
        [Authorize(Policy = Permissions.Products.Register)]
        public async Task<IActionResult> updatePromotion(UpdateFactorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("Sync")]
        [Authorize(Policy = Permissions.Products.Register)]
        public async Task<IActionResult> SyncProductAsync()
        {
            return Ok(await Mediator.Send(new SyncProductCommand()));
        }
    }
}