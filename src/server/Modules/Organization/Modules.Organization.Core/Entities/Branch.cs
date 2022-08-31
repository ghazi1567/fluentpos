using System;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Organization.Core.Entities
{
    public class Branch: IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime? CreateaAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public string EmailAddress { get; set; }

        public string Currency { get; set; }

        public string Country { get; set; }

        protected Branch()
        {
            Id = Guid.NewGuid();
        }
    }
}