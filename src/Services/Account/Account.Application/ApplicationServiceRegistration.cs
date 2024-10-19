using System.Reflection;
using Account.Application.IntegrationEvents.EventHandling;
using Account.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient<IIntegrationEventHandler<ClientDeletedEvent>, ClientDeletedEventHandler>();
            
        return services;
    }
}