using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Enums
{
    public enum OrderType : byte
    {
        StockIn = 1,
        StockOut = 2,
        StockReturn = 3,
        OpeningStock = 4
    }
}