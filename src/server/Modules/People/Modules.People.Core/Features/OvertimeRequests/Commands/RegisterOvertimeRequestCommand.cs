using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;

namespace FluentPOS.Modules.People.Core.Features.OvertimeRequests.Commands
{
    public class RegisterOvertimeRequestCommand : OvertimeRequestDto, IRequest<Result<Guid>>
    {
    }
}