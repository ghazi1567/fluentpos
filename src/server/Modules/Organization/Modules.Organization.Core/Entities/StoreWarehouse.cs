using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Organization.Core.Entities
{
    public class StoreWarehouse : BaseEntity
    {
        public long WarehouseId { get; set; }

        public Guid IdentityUserId { get; set; }
    }
}
