﻿// --------------------------------------------------------------------------------------------------
// <copyright file="FluentUser.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Modules.Identity.Core.Entities.ExtendedAttributes;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public class FluentUser : IdentityUser, IEntity<string>, IBaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CreatedBy { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public string RefreshToken { get; set; }

        public Guid? EmployeeId { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<UserExtendedAttribute> ExtendedAttributes { get; set; }

        private List<Event> _domainEvents;

        public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(Event domainEvent)
        {
            _domainEvents ??= new List<Event>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(Event domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public FluentUser()
            : base()
        {
            ExtendedAttributes = new HashSet<UserExtendedAttribute>();
        }
    }
}