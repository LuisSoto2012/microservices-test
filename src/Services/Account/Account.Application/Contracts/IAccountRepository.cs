using Account.Application.Contracts;

namespace Account.Application.Contracts;

public interface IAccountRepository : IAsyncRepository<Domain.Entities.Account>
{
    Task<Domain.Entities.Account?> GetByClientIdAsync(int clientId);
}