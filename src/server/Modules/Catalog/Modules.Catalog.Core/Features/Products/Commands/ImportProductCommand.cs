// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterProductCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    public class ImportProductCommand : IRequest<Result<long>>
    {
        public List<RegisterProductCommand> products { get; set; }
    }
}