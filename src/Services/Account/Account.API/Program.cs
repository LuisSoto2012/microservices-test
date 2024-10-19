using Account.API.Extensions;
using Account.Infrastructure.Persistance;

namespace Account.API;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build()
            .MigrateDatabase<AccountContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<AccountContextSeed>>();
                AccountContextSeed
                    .SeedAsync(context, logger)
                    .Wait();
            })
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}