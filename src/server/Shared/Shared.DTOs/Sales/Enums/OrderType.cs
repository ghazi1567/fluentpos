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
        OpeningStock = 4,
        Commited = 5,
        FulFill = 6,
        uncommitted = 7,
        StockUpdate = 8,
        SingleOrder = 9,
        SplittedOrder = 10,
    }
}