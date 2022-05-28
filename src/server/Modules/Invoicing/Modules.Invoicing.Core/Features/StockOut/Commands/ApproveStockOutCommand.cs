using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.StockIn.Commands
{

    public class ApproveStockOutCommand : IRequest<Result<Guid>>
    {
        public Guid OrderId { get;  set; }
        public OrderStatus Status { get; set; }
    }
}