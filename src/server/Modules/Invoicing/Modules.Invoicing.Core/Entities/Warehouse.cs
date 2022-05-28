using FluentPOS.Shared.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}