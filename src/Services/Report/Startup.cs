using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.OpenApi.Models;
using Report.Service.Domain.Events;
using Report.Service.EventBus;
using Report.Service.Services;

namespace Report.Service;

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
        // Agregar la conexión persistente de RabbitMQ
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetValue<string>("RabbitMQ:ConnectionString");
            return new RabbitMQPersistentConnection(connectionString);
        });

        // Registrar el manejador de suscripciones (sin interfaz)
        services.AddSingleton<EventBusSubscriptionsManager>();

        // Registrar el EventBus
        services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
        {
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var subscriptionManager = sp.GetRequiredService<EventBusSubscriptionsManager>();
            var serviceProvider = sp.GetRequiredService<IServiceProvider>();

            return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, subscriptionManager, serviceProvider);
        });
        
        services.AddScoped<IReportService, ReportService>();
        services.AddTransient<IIntegrationEventHandler<ClientCreatedEvent>, ClientCreatedEventHandler>();
        services.AddTransient<IIntegrationEventHandler<TransactionCreatedEvent>, TransactionCreatedEventHandler>();
        
        // Registrar el manejador de eventos en el EventBus
        var eventBus = services.BuildServiceProvider().GetRequiredService<IEventBus>();
        eventBus.Subscribe<ClientCreatedEvent, ClientCreatedEventHandler>();
        eventBus.Subscribe<TransactionCreatedEvent, TransactionCreatedEventHandler>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
            
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Report.API", Version = "v1"});
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report.API v1"));

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}