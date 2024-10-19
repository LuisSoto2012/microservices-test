using AutoMapper;
using Client.Application.Contracts;
using Client.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using MediatR;

namespace Client.Application.Features.Clients.Commands.AddClient;

public class AddClientCommandHandler : IRequestHandler<AddClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public AddClientCommandHandler(IClientRepository clientRepository, IMapper mapper, IEventBus eventBus)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }
    
    public async Task Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<Domain.Entities.Client>(request);
        
        var createdClient = await _clientRepository.AddAsync(client);

        await _eventBus.PublishAsync(new ClientCreatedEvent(createdClient.Id, createdClient.Name));
    }
}