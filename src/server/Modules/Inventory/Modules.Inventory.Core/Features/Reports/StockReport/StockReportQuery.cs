using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Filters;
using MediatR;
using System.Collections.Generic;

namespace FluentPOS.Modules.Inventory.Core.Features.Reports
{
    public class StockReportQuery : IRequest<PaginatedResult<StockDto>>
    {
        public string AdvancedSearchType { get; set; } = "And";

        public List<FilterModel> AdvanceFilters { get; set; }

        public List<SortModel> SortModel { get; set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public long[] WarehouseIds { get;  set; } = new long[0];

    }


}