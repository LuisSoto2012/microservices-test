using Microsoft.Extensions.Logging;

namespace Account.Infrastructure.Persistance;

public class AccountContextSeed
{
    public static async Task SeedAsync(AccountContext accountContext, ILogger<AccountContextSeed> logger)
    {
        // Seed accounts
        if (!accountContext.Accounts.Any())
        {
            accountContext.Accounts.AddRange(GetPreconfiguredAccounts());
            await accountContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(AccountContext).Name);
        }
    }

    private static IEnumerable<Domain.Entities.Account> GetPreconfiguredAccounts()
    {
        return new List<Domain.Entities.Account>
        {
            new Domain.Entities.Account
            {
                Number = "478758",
                Type = "Ahorros",
                InitialBalance = 1000.00m,
                CurrentBalance = 1000.00m,
                Status = true,
                ClientId = 1
            },
            new Domain.Entities.Account
            {
                Number = "478759",
                Type = "Corriente",
                InitialBalance = 2000.00m,
                CurrentBalance = 2000.00m,
                Status = true,
                ClientId = 2
            },
            new Domain.Entities.Account
            {
                Number = "478760",
                Type = "Ahorros",
                InitialBalance = 1500.00m,
                CurrentBalance = 1500.00m,
                Status = true,
                ClientId = 3
            },
            new Domain.Entities.Account
            {
                Number = "478761",
                Type = "Corriente",
                InitialBalance = 3000.00m,
                CurrentBalance = 3000.00m,
                Status = true,
                ClientId = 4
            },
            new Domain.Entities.Account
            {
                Number = "478762",
                Type = "Ahorros",
                InitialBalance = 1200.00m,
                CurrentBalance = 1200.00m,
                Status = true,
                ClientId = 5
            }
        };
    }
}