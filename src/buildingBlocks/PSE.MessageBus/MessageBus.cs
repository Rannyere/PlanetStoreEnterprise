using MassTransit;
using PSE.Core.Messages.Integration;
using System;
using System.Threading.Tasks;

namespace PSE.MessageBus;

public class MessageBus : IMessageBus, IDisposable
{
    private readonly IBusControl _bus;

    public MessageBus(string connectionString)
    {
        _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri(connectionString), h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        });

        _bus.StartAsync();
    }

    public bool IsConnected => _bus != null;

    public void Publish<T>(T message) where T : IntegrationEvent
    {
        _bus.Publish(message);
    }

    public async Task PublishAsync<T>(T message) where T : IntegrationEvent
    {
        await _bus.Publish(message);
    }

    public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
    {
        _bus.ConnectReceiveEndpoint(subscriptionId, cfg =>
        {
            cfg.Handler<T>(context =>
            {
                onMessage(context.Message);
                return Task.CompletedTask;
            });
        });
    }

    public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
    {
        _bus.ConnectReceiveEndpoint(subscriptionId, cfg =>
        {
            cfg.Handler<T>(context => onMessage(context.Message));
        });
    }

    public TResponse Request<TRequest, TResponse>(TRequest request)
        where TRequest : IntegrationEvent
        where TResponse : ResponseMessage
    {
        var client = _bus.CreateRequestClient<TRequest>();
        var response = client.GetResponse<TResponse>(request).GetAwaiter().GetResult();
        return response.Message;
    }

    public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
        where TRequest : IntegrationEvent
        where TResponse : ResponseMessage
    {
        var client = _bus.CreateRequestClient<TRequest>();
        var response = await client.GetResponse<TResponse>(request);
        return response.Message;
    }

    public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
        where TRequest : IntegrationEvent
        where TResponse : ResponseMessage
    {
        var handle = _bus.ConnectReceiveEndpoint($"respond-{typeof(TRequest).Name}", cfg =>
        {
            cfg.Handler<TRequest>(async context =>
            {
                var response = responder(context.Message);
                await context.RespondAsync(response);
            });
        });

        return new EndpointHandleAdapter(handle);
    }

    public async Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
        where TRequest : IntegrationEvent
        where TResponse : ResponseMessage
    {
        var handle = _bus.ConnectReceiveEndpoint($"{typeof(TRequest).Name}", cfg =>
        {
            cfg.Handler<TRequest>(async context =>
            {
                var response = await responder(context.Message);
                await context.RespondAsync(response);
            });
        });

        return new EndpointHandleAdapter(handle);
    }

    public void Dispose()
    {
        _bus?.Stop();
    }

    private class EndpointHandleAdapter : IDisposable
    {
        private readonly HostReceiveEndpointHandle _handle;

        public EndpointHandleAdapter(HostReceiveEndpointHandle handle)
        {
            _handle = handle;
        }

        public void Dispose()
        {
            _handle?.StopAsync().GetAwaiter().GetResult();
        }
    }
}
