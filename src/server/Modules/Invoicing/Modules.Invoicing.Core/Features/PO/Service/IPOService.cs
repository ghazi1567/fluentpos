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
        Task<Result<Guid>> Save(RegisterPOCommand command, CancellationToken cancellationToken);

        Task<Result<Guid>> Delete(RemovePOCommand request, CancellationToken cancellationToken);

        Task<Result<Guid>> Update(UpdatePOCommand request, CancellationToken cancellationToken);
    }
}