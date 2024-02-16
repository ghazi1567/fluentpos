using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos
{
    public class BaseEntityDto
    {
        public long Id { get; set; }

        public long? ShopifyId { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public long OrganizationId { get; set; }

        public long BranchId { get; set; }

        public long? UserId { get; set; }

        public string UserEmail { get; set; }

        public string IpAddress { get; set; }
    }
}