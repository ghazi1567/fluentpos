// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveExtendedAttributeCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands
{
    // ReSharper disable once UnusedTypeParameter
    public class RemoveExtendedAttributeCommand<TEntityId, TEntity> : IRequest<Result<long>>
        where TEntity : class, IEntity<TEntityId>
    {
        public long Id { get; }

        public RemoveExtendedAttributeCommand(long entityExtendedAttributeId)
        {
            Id = entityExtendedAttributeId;
        }
    }
}