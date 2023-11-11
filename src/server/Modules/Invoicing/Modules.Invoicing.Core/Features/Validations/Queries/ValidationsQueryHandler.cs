using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    internal class ValidationsQueryHandler :
                IRequestHandler<CheckValidCityQuery, bool>

    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalesQueryHandler> _localizer;
        private readonly IProductService _productService;
        private readonly IShopifyOrderFulFillmentService _shopifyOrderFulFillmentService;

        public ValidationsQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<SalesQueryHandler> localizer,
            IShopifyOrderFulFillmentService shopifyOrderFulFillmentService,
            IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
            _shopifyOrderFulFillmentService = shopifyOrderFulFillmentService;
        }

        public async Task<bool> Handle(CheckValidCityQuery request, CancellationToken cancellationToken)
        {
            return await _context.OperationCity.AnyAsync(x => x.CityName.ToLower() == request.CityName.ToLower());
        }
    }
}