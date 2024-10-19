using MediatR;

namespace Client.Application.Features.Clients.Queries.GetClientsList;

public class GetClientsListQuery : IRequest<List<ClientsVm>>
{
    public GetClientsListQuery()
    {
        
    }
}