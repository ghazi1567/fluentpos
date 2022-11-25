using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos.Dashboard
{
    public class DashboardDto
    {
        public int TotalEmployees { get; set; }

        public int Presents { get; set; }

        public int Absents { get; set; }


        public int LateComer { get; set; }

        public int Last7DaysLateComer { get; set; }

        public int Last7DaysAbsents { get; set; }

    }
}
