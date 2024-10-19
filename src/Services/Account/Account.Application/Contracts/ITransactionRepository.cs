using Account.Domain.Entities;

namespace Account.Application.Contracts;

public interface ITransactionRepository : IAsyncRepository<Domain.Entities.Transaction>
{
    Task<IEnumerable<Transaction>> GetTransactionsByAccountAsync(int accountId);
}