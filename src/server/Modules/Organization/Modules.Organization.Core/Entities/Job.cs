using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Core.Entities
{
    public class Job : BaseEntity
    {
        public JobType JobName { get; set; }

        public string Schedule { get; set; }

        public bool Enabled { get; set; }

    }
}