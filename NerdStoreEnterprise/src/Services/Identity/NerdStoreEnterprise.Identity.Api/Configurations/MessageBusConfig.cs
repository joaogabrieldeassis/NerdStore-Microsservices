using NerdStoreEnterprise.Core.Utils;
using NerdStoreEnterprise.MessageBus;

namespace NerdStoreEnterprise.Identity.Api.Configurations;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus")!);
    }
}
