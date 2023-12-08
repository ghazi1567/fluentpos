using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.StockIn.Commands
{
    public class RemoveStockOutCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }

        public RemoveStockOutCommand(long id)
        {
            Id = id;
        }
    }
}
