using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.PO.Service
{
    public interface IPOService
    {
        Task<bool> AlreadyExist(long id);

        Task<Result<long>> Save(RegisterPOCommand command, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SavePurchaseOrder(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<long>> Delete(RemovePOCommand request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<long>> Update(UpdatePOCommand request, CancellationToken cancellationToken = default(CancellationToken));
    }
}