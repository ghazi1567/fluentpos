using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IWorkFlowService
    {
        Task<bool> AssignAprroversAsync(Guid requestId);

        Task<bool> ApproveRequestAsync(Guid requestId, Guid approverId, string comments);

        Task<bool> RejectRequestAsync(Guid requestId, Guid approverId, string comments);

    }
}
