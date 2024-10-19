using Account.Application.Contracts;
using Account.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Repositories;

public class AccountRepository : RepositoryBase<Domain.Entities.Account>, IAccountRepository
{
    public AccountRepository(AccountContext dbContext) : base(dbContext)
    {
    }

    public async Task<Domain.Entities.Account?> GetByClientIdAsync(int clientId)
    {
        return await _dbContext.Set<Domain.Entities.Account>().FirstOrDefaultAsync(a => a.ClientId == clientId);
    }
}