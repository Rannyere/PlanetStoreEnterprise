using FluentValidation.Results;
using MediatR;
using PSE.Core.Messages;
using System.Threading.Tasks;

namespace PSE.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvent<T>(T publishEvent) where T : Event
    {
        await _mediator.Publish(publishEvent);
    }

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }
}