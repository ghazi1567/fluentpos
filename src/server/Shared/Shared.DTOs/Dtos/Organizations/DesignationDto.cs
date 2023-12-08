using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos.Organizations
{
    public class DesignationDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long OrganizationId { get; set; }

        public long BranchId { get; set; }

    }
}