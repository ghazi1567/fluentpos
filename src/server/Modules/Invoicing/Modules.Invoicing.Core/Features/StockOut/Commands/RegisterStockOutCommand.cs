using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.StockIn.Commands
{

    public class RegisterStockOutCommand : IRequest<Result<long>>
    {
        public DateTime TimeStamp { get;  set; }

        public string Note { get;  set; }

        public string ReferenceNumber { get; set; }

        public List<Product> Products { get;  set; }

        public long WarehouseId { get; set; }
    }
}