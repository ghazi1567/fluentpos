// --------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentPOS.Shared.Core.Contracts;

namespace FluentPOS.Shared.Core.Domain
{
    public abstract class BaseEntity : IEntity<Guid>, IBaseEntity
    {
        [Key]
        public Guid UUID { get; set; }

        public long? Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid BranchId { get; set; }

        protected BaseEntity()
        {
            UUID = Guid.NewGuid();
        }

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
    }
}