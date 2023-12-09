using FluentPOS.Shared.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public class UserBranch : BaseEntity
    {
        public Guid IdentityUserId { get; set; }

        public bool Active { get; set; }

        [NotMapped]
        public bool Selected { get; set; }
    }
}