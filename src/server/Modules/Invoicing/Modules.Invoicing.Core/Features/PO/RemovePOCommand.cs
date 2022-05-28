using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.PO
{
    public class RemovePOCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public RemovePOCommand(Guid productId)
        {
            Id = productId;
        }
    }
}
