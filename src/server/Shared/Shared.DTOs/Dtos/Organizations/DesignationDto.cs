﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos.Organizations
{
    public class DesignationDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid BranchId { get; set; }

    }
}