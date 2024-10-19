using Client.API.Extensions;
using Client.Infrastructure.Persistance;

namespace Client.API;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build()
            .MigrateDatabase<ClientContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<ClientContextSeed>>();
                ClientContextSeed
                    .SeedAsync(context, logger)
                    .Wait();
            })
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}