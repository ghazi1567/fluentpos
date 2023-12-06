using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{

    internal class DashboardQueryHandler : IRequestHandler<GetDashboardQuery, Result<DashboardDto>>
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DashboardQueryHandler> _localizer;

        public DashboardQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<DashboardQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<DashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.FulfillmentOrders.AsNoTracking().AsQueryable();

            if (request.WarehouseId.HasValue)
            {
                queryable = queryable.Where(x => x.WarehouseId == request.WarehouseId.Value);
            }

            var dashboardDto = new DashboardDto();

            dashboardDto.pending = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.PendingApproval || x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.Pending).CountAsync();
            dashboardDto.reQueued = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.ReQueueAfterReject).CountAsync();
            dashboardDto.assignedToOutlet = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.AssignToOutlet).CountAsync();
            dashboardDto.assignedToHeadOffice = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.AssignToHeadOffice).CountAsync();
            dashboardDto.approved = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.Approved).CountAsync();
            dashboardDto.shipped = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.Shipped).CountAsync();
            dashboardDto.preparing = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.Preparing).CountAsync();
            dashboardDto.readyToShip = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.ReadyToShip).CountAsync();
            dashboardDto.verifying = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.Verifying).CountAsync();
            dashboardDto.cityCorrection = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.CityCorrection).CountAsync();
            dashboardDto.cancelled = await queryable.Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.Cancelled).CountAsync();

            return await Result<DashboardDto>.SuccessAsync(dashboardDto);
        }
    }
}