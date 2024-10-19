using Client.Application.Contracts;
using Client.Infrastructure.Persistance;
using Client.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ClientContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ClientConnectionString")));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IClientRepository, ClientRepository>();
            
        return services;
    }
}