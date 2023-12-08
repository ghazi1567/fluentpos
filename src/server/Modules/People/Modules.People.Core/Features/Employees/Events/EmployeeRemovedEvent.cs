// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerRemovedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Features.Customers.Events
{
    public class EmployeeRemovedEvent : Event
    {
        public long Id { get; }

        public EmployeeRemovedEvent(long id)
        {
            Id = id;
            AggregateId = id;
            RelatedEntities = new[] { typeof(Customer) };
        }
    }
}