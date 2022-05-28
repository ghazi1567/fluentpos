using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.StockIn.Commands
{
    public class RemoveStockInCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public RemoveStockInCommand(Guid id)
        {
            Id = id;
        }
    }
}
