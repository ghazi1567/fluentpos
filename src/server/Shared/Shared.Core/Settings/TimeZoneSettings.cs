using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Settings
{
    public class TimeZoneSettings
    {
        public bool SupportMultipleTimeZones { get; set; }

        public string SiteTimeZoneId { get; set; }
    }
}
