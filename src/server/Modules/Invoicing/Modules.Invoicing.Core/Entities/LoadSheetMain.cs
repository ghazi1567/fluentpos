using FluentPOS.Shared.Core.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    [Table("LoadSheetMains")]
    public class LoadSheetMain : BaseEntity
    {
        public long TotalOrder { get; set; }

        public decimal TotalAmount { get; set; }

        public IEnumerable<LoadSheetDetail> Details { get; set; }
    }
}