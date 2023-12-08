using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.StockIn.Commands;
using FluentPOS.Shared.Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.PO.Service
{
    public interface IStockInService
    {
        Task<bool> AlreadyExist(long id);

        Task<Result<long>> Save(RegisterStockInCommand command, CancellationToken cancellationToken);

        Task<bool> SaveStockIn(InternalOrder order, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<long>> Approve(ApproveStockInCommand request, CancellationToken cancellationToken);

        Task<Result<long>> Delete(RemoveStockInCommand request, CancellationToken cancellationToken);

        Task<Result<long>> Update(UpdateStockInCommand request, CancellationToken cancellationToken);
    }
}