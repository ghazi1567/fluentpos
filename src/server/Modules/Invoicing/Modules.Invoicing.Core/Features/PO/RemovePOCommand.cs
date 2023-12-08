using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.PO
{
    public class RemovePOCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }

        public RemovePOCommand(long productId)
        {
            Id = productId;
        }
    }
}
