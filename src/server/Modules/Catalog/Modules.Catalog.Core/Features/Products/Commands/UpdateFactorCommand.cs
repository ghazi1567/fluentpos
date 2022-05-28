using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    public class UpdateFactorCommand : IRequest<Result<Guid>>
    {
        public List<ProductFactorDto> Products { get; set; }

        public DateTime updateFrom { get; set; }
    }
}