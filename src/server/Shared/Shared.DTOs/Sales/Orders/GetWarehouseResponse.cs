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
             long? ShopifyId,
             string Name,
             bool? Active,
             Guid? ParentId,
             string Latitude,
             string Longitude,
             string Phone,
             string Address1,
             string City,
             string Code,
             string PickupAddress
      );
}