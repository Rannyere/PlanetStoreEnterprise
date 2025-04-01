using MassTransit;
using PSE.Core.Messages.Integration;
using System;
using System.Threading.Tasks;

namespace PSE.MessageBus
{
    public class MessageBus : IMessageBus, IDisposable
    {
        private IBusControl _bus;
        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        public bool IsConnected => _bus?.CheckHealth().Status == BusHealthStatus.Healthy;

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            _bus.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.Publish(message);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.ConnectReceiveEndpoint(subscriptionId, cfg =>
            {
                cfg.Handler<T>(context =>
                {
                    onMessage(context.Message);
                    return Task.CompletedTask;
                });
            });
        }

        public async Task<HostReceiveEndpointHandle> SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            var handler = _bus.ConnectReceiveEndpoint(subscriptionId, cfg =>
            {
                cfg.Handler<T>(context =>
                    onMessage(context.Message));
            });
            return await Task.FromResult(handler);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            var client = _bus.CreateRequestClient<TRequest>();
            var response = client.GetResponse<TResponse>(request).GetAwaiter().GetResult();
            return response.Message;
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            var client = _bus.CreateRequestClient<TRequest>();
            var response = await client.GetResponse<TResponse>(request);
            return response.Message;
        }

        public HostReceiveEndpointHandle Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            var handle = _bus.ConnectReceiveEndpoint($"{typeof(TRequest).Name}", cfg =>
            {
                cfg.Handler<TRequest>(async context =>
                {
                    var response = responder(context.Message);
                    await context.RespondAsync(response);
                });
            });
            return handle;
        }

        public async Task<HostReceiveEndpointHandle> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            var handle = _bus.ConnectReceiveEndpoint($"{typeof(TRequest).Name}", cfg =>
            {
                cfg.Handler<TRequest>(async context =>
                {
                    var response = await responder(context.Message);
                    await context.RespondAsync(response);
                });
            });

            return await Task.FromResult(handle);
        }

        private void TryConnect()
        {
            if (IsConnected) return;

            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(_connectionString);
            });

            _bus?.StartAsync();
        }

        public void Dispose()
        {
            _bus?.Stop();
        }
    }
}
