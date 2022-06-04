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
        Task<bool> AlreadyExist(Guid id);

        Task<Result<Guid>> Save(RegisterPOCommand command, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SavePurchaseOrder(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<Guid>> Delete(RemovePOCommand request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<Guid>> Update(UpdatePOCommand request, CancellationToken cancellationToken = default(CancellationToken));
    }
}