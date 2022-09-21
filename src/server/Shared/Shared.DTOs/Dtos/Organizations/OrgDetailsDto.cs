using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos.Organizations
{
    public class OrgDetailsDto
    {
        public OrganizationDto Organization { get; set; }

        public BranchDto Branch { get; set; }

        public DepartmentDto Department { get; set; }

        public DesignationDto Designation  { get; set; }

        public PolicyDto Policy { get; set; }

    }
}