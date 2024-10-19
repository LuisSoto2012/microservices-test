using Client.Application.Contracts;
using Client.Infrastructure.Persistance;

namespace Client.Infrastructure.Repositories;

public class ClientRepository : RepositoryBase<Domain.Entities.Client>, IClientRepository
{
    public ClientRepository(ClientContext dbContext) : base(dbContext)
    {
    }
}