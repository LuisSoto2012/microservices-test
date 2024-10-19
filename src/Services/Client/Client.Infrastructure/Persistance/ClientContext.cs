using Client.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Client.Infrastructure.Persistance;

public class ClientContext : DbContext
{
    public ClientContext(DbContextOptions<ClientContext> options) : base(options)
    {
    }
    
    public DbSet<Domain.Entities.Client> Clients { get; set; }
    
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