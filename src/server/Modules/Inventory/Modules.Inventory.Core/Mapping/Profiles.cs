using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Modules.Inventory.Core.Entities;
using FluentPOS.Modules.Inventory.Core.Features.Levels;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Sales.Orders;

namespace FluentPOS.Modules.Inventory.Core.Mapping
{
     public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<StockDto, Stock>().ReverseMap();
            CreateMap<ImportFileDto, ImportFile>().ReverseMap();
            CreateMap<ImportRecordDto, ImportRecord>().ReverseMap();

            CreateMap<ShopifySharp.InventoryLevel, InternalInventoryLevel>(MemberList.Source)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}