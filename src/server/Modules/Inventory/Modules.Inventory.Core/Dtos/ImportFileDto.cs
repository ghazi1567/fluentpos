using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Upload;
using System.Collections.Generic;

namespace FluentPOS.Modules.Inventory.Core.Dtos
{
    public class ImportFileDto : BaseEntityDto
    {
        public string FileName { get; set; }

        public string Extension { get; set; }

        public UploadType UploadType { get; set; }

        public string Status { get; set; } = "Pending";

        public string Note { get; set; }

        public IEnumerable<ImportRecordDto> ImportRecords { get; set; }
    }
}