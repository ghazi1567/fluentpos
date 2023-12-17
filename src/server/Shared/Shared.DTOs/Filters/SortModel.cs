using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Filters
{
    public class SortModel
    {
        public string Key { get; set; }

        public string Sort { get; set; } = "asc";
    }
}
