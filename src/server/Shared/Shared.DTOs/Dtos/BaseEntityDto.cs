using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos
{
    public class BaseEntityDto
    {
        public Guid? Id { get; set; }

        public long? ShopifyId { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid BranchId { get; set; }

        public Guid? UserId { get; set; }

        public string IpAddress { get; set; }
    }
}