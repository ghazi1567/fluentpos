using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.Organization.Core.Dtos
{
    public class DepartmentDto : BaseEntityDto
    {
        public string Name { get; set; }

        public bool IsGlobalDepartment { get; set; }

        public string Description { get; set; }

        public long? HeadOfDepartment { get; set; }

        public int Production { get; set; }

        public long PolicyId { get; set; }

        public long? ParentId { get; set; }

        public string ParentDept { get; set; }
    }
}