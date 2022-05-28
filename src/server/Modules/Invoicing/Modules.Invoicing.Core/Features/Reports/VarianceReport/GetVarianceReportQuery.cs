using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Reports.VarianceReport
{
    public class GetVarianceReportQuery : IRequest<Result<List<VarianceProductReport>>>
    {
        public List<string> Barcodes { get; set; }
    }
}