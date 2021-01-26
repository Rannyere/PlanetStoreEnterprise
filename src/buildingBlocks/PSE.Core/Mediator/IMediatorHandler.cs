using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using PSE.Core.Messages;

namespace PSE.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T publishEvent) where T : Event;

        Task<ValidationResult> SendCommand<T>(T comand) where T : Command;
    }
}