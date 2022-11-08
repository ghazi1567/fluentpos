using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Core.Dtos
{
    public class JobDto : BaseEntityDto
    {
        public JobType JobName { get; set; }

        public string Schedule { get; set; }

        public bool Enabled { get; set; }

    }
}