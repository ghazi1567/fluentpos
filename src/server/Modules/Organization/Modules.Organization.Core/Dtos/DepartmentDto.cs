using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.Organization.Core.Dtos
{
    public class DepartmentDto : BaseEntityDto
    {
        public string Name { get; set; }

        public bool IsGlobalDepartment { get; set; }

        public string Description { get; set; }

        public Guid? HeadOfDepartment { get; set; }

        public int Production { get; set; }

        public Guid PolicyId { get; set; }

        public Guid? ParentId { get; set; }

        public string ParentDept { get; set; }
    }
}