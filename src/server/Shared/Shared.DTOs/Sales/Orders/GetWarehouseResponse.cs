using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
      public record GetWarehouseResponse
      (
             Guid Id,
             string Name,
             bool Active
      );
}