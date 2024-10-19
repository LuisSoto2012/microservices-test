using Account.Application.Contracts;
using Account.Domain.Entities;
using Account.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Repositories;

public class TransactionRepository : RepositoryBase<Domain.Entities.Transaction>, ITransactionRepository
{
    public TransactionRepository(AccountContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountAsync(int accountId)
    {
        return  await _dbContext.Transactions
            .Where(t => t.AccountId == accountId && t.Date < DateTime.Now)
            .ToListAsync();
    }
}