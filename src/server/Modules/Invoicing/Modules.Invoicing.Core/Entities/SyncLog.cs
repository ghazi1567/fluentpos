using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class SyncLog : BaseEntity
    {
        public long RemoteClientId { get;  set; }

        public long EntryId { get;  set; }

        public string EntryType { get;  set; }

        public DateTime LastUpdateOn { get;  set; }

        public string Status { get; set; } = "C";


    }
}