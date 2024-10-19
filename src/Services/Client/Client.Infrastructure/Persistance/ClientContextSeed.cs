using Microsoft.Extensions.Logging;

namespace Client.Infrastructure.Persistance;

public class ClientContextSeed
{
    public static async Task SeedAsync(ClientContext clientContext, ILogger<ClientContextSeed> logger)
    {
        if (!clientContext.Clients.Any())
        {
            clientContext.Clients.AddRange(GetPreconfiguredClients());
            await clientContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ClientContext).Name);
        }
    }

    private static IEnumerable<Domain.Entities.Client> GetPreconfiguredClients()
    {
        return new List<Domain.Entities.Client>
        {
            new Domain.Entities.Client
            {
                Name = "Mehmet Ozkaya", 
                Gender = "Male", 
                Age = 30, 
                IdentificationNumber = "TR12345678901",
                Address = "Bahcelievler, Istanbul", 
                Phone = "+90 123 456 7890",
                Password = "password123", 
                Status = true
            },
            new Domain.Entities.Client
            {
                Name = "John Doe", 
                Gender = "Male", 
                Age = 28, 
                IdentificationNumber = "US987654321", 
                Address = "123 Main St, New York", 
                Phone = "+1 234 567 8901",
                Password = "securepassword", 
                Status = true
            },
            new Domain.Entities.Client
            {
                Name = "Anna Smith", 
                Gender = "Female", 
                Age = 25, 
                IdentificationNumber = "CA192837465", 
                Address = "456 Oak St, Toronto", 
                Phone = "+1 234 567 8902",
                Password = "mypassword", 
                Status = true
            },
            new Domain.Entities.Client
            {
                Name = "Paul Miller", 
                Gender = "Male", 
                Age = 35, 
                IdentificationNumber = "DE123456789", 
                Address = "789 Pine St, Berlin", 
                Phone = "+49 123 456 7893",
                Password = "anotherpassword", 
                Status = false
            },
            new Domain.Entities.Client
            {
                Name = "Linda Johnson", 
                Gender = "Female", 
                Age = 40, 
                IdentificationNumber = "AU567890123", 
                Address = "101 Maple St, Sydney", 
                Phone = "+61 234 567 8904",
                Password = "password2023", 
                Status = true
            }
        };
    }
}