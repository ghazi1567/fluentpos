using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Logistics;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.StockIn.Queries
{

    internal class LoadsheetQueryHandler : IRequestHandler<GetLoadsheetInByIdQuery, Result<LoadSheetMainDto>>,
        IRequestHandler<GetLoadsheetsQuery, Result<List<LoadSheetMainDto>>>
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<StockInQueryHandler> _localizer;
        private readonly IPostexService _postexService;

        public LoadsheetQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<StockInQueryHandler> localizer,
            IPostexService postexService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _postexService = postexService;
        }

        public async Task<Result<LoadSheetMainDto>> Handle(GetLoadsheetInByIdQuery request, CancellationToken cancellationToken)
        {
            var loadSheet = await _context.LoadSheetMains.AsNoTracking()
                .Include(x => x.Details)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (loadSheet == null)
            {
                throw new SalesException(_localizer["Loadsheet Not Found!"], HttpStatusCode.NotFound);
            }

            var dto = _mapper.Map<LoadSheetMainDto>(loadSheet);
            return await Result<LoadSheetMainDto>.SuccessAsync(data: dto);
        }

        public async Task<Result<List<LoadSheetMainDto>>> Handle(GetLoadsheetsQuery request, CancellationToken cancellationToken)
        {
            var loadSheet = await _context.LoadSheetMains.AsNoTracking().ToListAsync();

            if (loadSheet == null)
            {
                throw new SalesException(_localizer["Loadsheets Not Found!"], HttpStatusCode.NotFound);
            }

            var dto = _mapper.Map<List<LoadSheetMainDto>>(loadSheet);
            return await Result<List<LoadSheetMainDto>>.SuccessAsync(data: dto);
        }
    }
}