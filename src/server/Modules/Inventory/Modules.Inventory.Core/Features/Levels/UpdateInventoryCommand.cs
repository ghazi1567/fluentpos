using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;
using System;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class UpdateInventoryCommand : IRequest<Result<bool>>
    {
    }
}