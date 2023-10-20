using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Core.Dtos
{
    public class BranchDto
    {
        public Guid? Id { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public string EmailAddress { get; set; }

        public string Currency { get; set; }

        public string Country { get; set; }

        public string ShopifyUrl { get; set; }

        public string AccessToken { get; set; }

        public string ShopifyAdminEmail { get; set; }

        // TODO: password should be encrypted
        public string ShopifyAdminPassword { get; set; }

    }
}