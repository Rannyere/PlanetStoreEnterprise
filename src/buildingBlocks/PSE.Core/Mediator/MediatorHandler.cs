using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using PSE.Core.Messages;

namespace PSE.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishEvent<T>(T publishEvent) where T : Event
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> SendCommand<T>(T comand) where T : Command
        {
            throw new NotImplementedException();
        }
    }
}
