using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Modules.Inventory.Core.Entities;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Sales.Orders;

namespace FluentPOS.Modules.Inventory.Core.Mapping
{
     public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<StockDto, Stock>().ReverseMap();
        }
    }
}