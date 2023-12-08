// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerExtendedAttributesController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers.ExtendedAttributes
{
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(Customer) + "/attributes")]
    internal sealed class CustomerExtendedAttributesController : ExtendedAttributesController<long, Customer>
    {
        [Authorize(Policy = Permissions.CustomersExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<long, Customer> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<long, ExtendedAttribute<long, Customer>> filter)
        {
            return base.GetByIdAsync(filter);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Add)]
        public override Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<long, Customer> command)
        {
            return base.CreateAsync(command);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Update)]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<long, Customer> command)
        {
            return base.UpdateAsync(command);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Remove)]
        public override Task<IActionResult> RemoveAsync(long id)
        {
            return base.RemoveAsync(id);
        }
    }
}