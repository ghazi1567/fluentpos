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
    public abstract class BaseEntity : IEntity<long>, IBaseEntity
    {
        [Key]
        public long Id { get; set; }

        public long? ShopifyId { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public long OrganizationId { get; set; }

        public long BranchId { get; set; }

        public string UserEmail { get; set; }

        protected BaseEntity()
        {
           // Id = default(long);
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