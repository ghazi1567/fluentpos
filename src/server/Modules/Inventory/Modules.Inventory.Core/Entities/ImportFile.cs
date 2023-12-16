using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Upload;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class ImportFile : BaseEntity
    {
        public string FileName { get; set; }

        public string Extension { get; set; }

        public UploadType UploadType { get; set; }

        public string Status { get; set; } = "Pending";

        public string Note { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public IEnumerable<ImportRecord> ImportRecords { get; set; }
    }
}