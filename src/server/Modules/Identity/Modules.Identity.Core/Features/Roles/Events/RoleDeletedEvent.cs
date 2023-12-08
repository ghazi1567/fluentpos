// --------------------------------------------------------------------------------------------------
// <copyright file="RoleDeletedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Identity.Core.Features.Roles.Events
{
    public class RoleDeletedEvent : Event
    {
        public string Id { get; }

        public RoleDeletedEvent(string id)
        {
            Id = id;
            //AggregateId = long.TryParse(id, out var aggregateId)
            //    ? aggregateId
            //    : default(long); TODO
            RelatedEntities = new[] { typeof(FluentRole) };
        }
    }
}