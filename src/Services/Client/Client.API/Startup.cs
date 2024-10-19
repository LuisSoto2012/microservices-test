using Client.Application;
using Client.Infrastructure;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.OpenApi.Models;

namespace Client.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add RabbitMQ connection
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetValue<string>("RabbitMQ:ConnectionString");
            return new RabbitMQPersistentConnection(connectionString);
        });
        
        services.AddSingleton<EventBusSubscriptionsManager>();

        // EventBus config
        services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
        {
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var subscriptionManager = sp.GetRequiredService<EventBusSubscriptionsManager>();
            var serviceProvider = sp.GetRequiredService<IServiceProvider>();

            return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, subscriptionManager, serviceProvider);
        });
        
        services.AddApplicationServices();
        services.AddInfrastructureServices(Configuration);

        services.AddControllers();
        services.AddSwaggerGen(c =>
            
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Client.API", Version = "v1"});
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client.API v1"));

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}