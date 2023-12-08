using FluentPOS.Shared.Core.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace FluentPOS.Modules.Organization.Core.Entities
{
    public class Organisation : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public string EmailAddress { get; set; }

        public string Currency { get; set; }

        public string Country { get; set; }

        protected Organisation()
        {
            //Id = default(long);
        }
    }
}