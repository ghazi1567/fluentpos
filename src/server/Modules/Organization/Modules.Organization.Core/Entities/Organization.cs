using System;

namespace FluentPOS.Modules.Organization.Core.Entities
{
    public class Organization
    {
        public Guid Id { get; set; }

        public DateTime? CreateaAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public string EmailAddress { get; set; }

        public string Currency { get; set; }

        public string Country { get; set; }

        protected Organization()
        {
            Id = Guid.NewGuid();
        }
    }
}