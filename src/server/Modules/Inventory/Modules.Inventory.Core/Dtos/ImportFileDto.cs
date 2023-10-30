using FluentPOS.Shared.DTOs.Upload;
using System.Collections.Generic;

namespace FluentPOS.Modules.Inventory.Core.Dtos
{
    public class ImportFileDto
    {
        public string FileName { get; set; }

        public string Extension { get; set; }

        public UploadType UploadType { get; set; }

        public IEnumerable<ImportRecordDto> ImportRecords { get; set; }
    }
}