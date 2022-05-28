using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Reports.VarianceReport
{
    internal class GetVarianceReportHandler : IRequestHandler<GetVarianceReportQuery, Result<List<VarianceProductReport>>>
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetVarianceReportHandler> _localizer;
        private readonly IProductService _productService;

        public GetVarianceReportHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<GetVarianceReportHandler> localizer,
            IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
        }

        public async Task<Result<List<VarianceProductReport>>> Handle(GetVarianceReportQuery request, CancellationToken cancellationToken)
        {
            string barcodes = string.Join(",", request.Barcodes);
            string query = string.Format(
                @"SELECT P.Id ProductId,ISNULL(S.AvailableQuantity,0) AvailableQuantity,ISNULL(s.LastUpdatedOn,GETDATE())LastUpdatedOn,p.Name,p.discountFactor,p.BarcodeSymbology
                FROM [Catalog].[Products] P Left JOIN [Inventory].[Stocks] S ON P.Id = S.ProductId
                where p.productCode in ({0})", barcodes);

            var result = await _context.ExecuteQueryAsync<VarianceProductReportDto>(query);
            return await Result<List<VarianceProductReport>>.SuccessAsync(data: _mapper.Map<List<VarianceProductReport>>(result));
        }
    }
}