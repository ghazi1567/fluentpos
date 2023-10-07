using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;

namespace FluentPOS.Modules.People.Core.Features.OvertimeRequests.Commands
{
    public class RemoveOvertimeRequestCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public RemoveOvertimeRequestCommand(Guid customerId)
        {
            Id = customerId;
        }
    }
}
