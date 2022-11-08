﻿using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Organization.Core.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }

        public bool IsGlobalDepartment { get; set; }

        public string Description { get; set; }

        public Guid? HeadOfDepartment { get; set; }

        public int Production { get; set; }
    }
}