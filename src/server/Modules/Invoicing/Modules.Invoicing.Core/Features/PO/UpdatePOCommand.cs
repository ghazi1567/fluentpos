using System;
using System.Collections.Generic;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.Invoicing.Core.Features.PO
{

    public class UpdatePOCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public string Note { get; set; }

        public string ReferenceNumber { get; set; }

        public List<Product> Products { get; set; }

        public Guid WarehouseId { get; set; }
    }
}