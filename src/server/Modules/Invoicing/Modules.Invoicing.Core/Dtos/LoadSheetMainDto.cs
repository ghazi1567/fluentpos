using FluentPOS.Shared.DTOs.Dtos;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class LoadSheetMainDto : BaseEntityDto
    {
        public long TotalOrder { get; set; }

        public decimal TotalAmount { get; set; }

        public IEnumerable<LoadSheetDetailDto> Details { get; set; }
    }
}