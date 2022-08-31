using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Organization.Core.Entities
{
    public class Designation : BaseEntity
    {
        public string Name { get; set; }

        public Guid DepartmentId { get; set; }
    }
}