using AutoMapper;
using Client.Application.Contracts;
using MediatR;

namespace Client.Application.Features.Clients.Queries.GetClientsList;

public class GetClientsListQueryHandler : IRequestHandler<GetClientsListQuery, List<ClientsVm>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    
    public GetClientsListQueryHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<List<ClientsVm>> Handle(GetClientsListQuery request, CancellationToken cancellationToken)
    {
        var clientList = await _clientRepository.GetAllAsync();
        return _mapper.Map<List<ClientsVm>>(clientList);
    }
}