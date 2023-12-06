using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetDashboardQuery : IRequest<Result<DashboardDto>>
    {
        public Guid? WarehouseId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}