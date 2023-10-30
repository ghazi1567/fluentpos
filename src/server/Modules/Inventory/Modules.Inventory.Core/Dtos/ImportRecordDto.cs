using FluentPOS.Shared.DTOs.Dtos;

namespace FluentPOS.Modules.Inventory.Core.Dtos
{
    public class ImportRecordDto : BaseEntityDto
    {
        public string Location { get; set; }

        public string SKU { get; set; }

        public int Qty { get; set; }

        public string Warehouse { get; set; }
    }
}