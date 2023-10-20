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
    public interface IStockOutService
    {
        Task<bool> AlreadyExist(Guid id);

        Task<bool> Save(InternalOrder order, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<Guid>> Update(UpdateStockOutCommand request, CancellationToken cancellationToken);

        Task<Result<Guid>> Delete(RemoveStockOutCommand request, CancellationToken cancellationToken);

        Task<Result<Guid>> Save(RegisterStockOutCommand command, CancellationToken cancellationToken);
    }
}