using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IWorkFlowService
    {
        Task<bool> AssignAprroversAsync(long requestId);

        Task<bool> ApproveRequestAsync(long requestId, long approverId, string comments);

        Task<bool> RejectRequestAsync(long requestId, long approverId, string comments);

    }
}
