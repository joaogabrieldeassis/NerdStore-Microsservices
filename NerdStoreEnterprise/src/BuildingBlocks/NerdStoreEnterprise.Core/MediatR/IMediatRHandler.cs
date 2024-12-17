using FluentValidation.Results;
using NerdStoreEnterprise.Core.Messages;

namespace NerdStoreEnterprise.Core.MediatR;

public interface IMediatRHandler
{
    public Task PublishEvent<T>(T eventp) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}