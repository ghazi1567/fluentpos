using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.Organization.Core.Dtos
{
    public class DesignationDto : BaseEntityDto
    {
        public string Name { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
