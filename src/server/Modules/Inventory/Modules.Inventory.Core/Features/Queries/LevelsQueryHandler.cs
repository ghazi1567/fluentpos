using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Modules.Inventory.Core.Features.Queries;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class LevelsQueryHandler : IRequestHandler<GetImportFilesQuery, PaginatedResult<ImportFileDto>>
    {
        private readonly IStringLocalizer<LevelsQueryHandler> _localizer;
        private readonly IMapper _mapper;
        private readonly IInventoryDbContext _context;
        private readonly IShopifyInventoryService _shopifyInventoryService;
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;

        public LevelsQueryHandler(
            IStringLocalizer<LevelsQueryHandler> localizer,
            IMapper mapper,
            IInventoryDbContext context,
            IShopifyInventoryService shopifyInventoryService,
            IProductService productService,
            IWarehouseService warehouseService)
        {
            _localizer = localizer;
            _mapper = mapper;
            _context = context;
            _shopifyInventoryService = shopifyInventoryService;
            _productService = productService;
            _warehouseService = warehouseService;
        }

        public async Task<PaginatedResult<ImportFileDto>> Handle(GetImportFilesQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.ImportFiles.AsNoTracking()
                .Include(x => x.ImportRecords)
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            var importFiles = await queryable
            .AsNoTracking()
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedResult<ImportFileDto>>(importFiles);
        }
    }
}