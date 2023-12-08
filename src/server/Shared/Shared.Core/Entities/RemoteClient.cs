using System;

namespace FluentPOS.Shared.Core.Entities
{
    public class RemoteClient
    {
        protected RemoteClient()
        {
            LastUpdateOn = DateTime.Now;
        }

        public long Id { get; set; }

        public string ClientName { get; private set; }

        public string ClientLocation { get; private set; }

        public DateTime LastUpdateOn { get; private set; }

        public bool Enabled { get; private set; }
    }
}