using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.PO
{

    public class RegisterPOCommand : IRequest<Result<Guid>>
    {

        public string Note { get;  set; }

        public string ReferenceNumber { get; set; }

        public List<Product> Products { get;  set; }

        public Guid WarehouseId { get; set; }
    }
}