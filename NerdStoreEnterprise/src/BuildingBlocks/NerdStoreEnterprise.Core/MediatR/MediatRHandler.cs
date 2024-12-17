using FluentValidation.Results;
using MediatR;
using NerdStoreEnterprise.Core.Messages;

namespace NerdStoreEnterprise.Core.MediatR;

public class MediatRHandler(IMediator mediator) : IMediatRHandler
{
    private readonly IMediator _mediator = mediator;

    

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
       return await _mediator.Send(command);
    }

    public async Task PublishEvent<T>(T eventp) where T : Event
    {
        await _mediator.Publish(eventp);
    }
}