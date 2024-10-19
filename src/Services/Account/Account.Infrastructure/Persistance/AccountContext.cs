using System.Transactions;
using Account.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Persistance;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
    }
    
    public DbSet<Domain.Entities.Account> Accounts { get; set; }
    public DbSet<Domain.Entities.Transaction> Transactions { get; set; }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "lsotof";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "lsotof";
                    break;
            }
        }
            
        return base.SaveChangesAsync(cancellationToken);
    }
}