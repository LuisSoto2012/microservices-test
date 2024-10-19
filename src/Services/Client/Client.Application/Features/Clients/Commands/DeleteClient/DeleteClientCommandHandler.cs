using AutoMapper;
using Client.Application.Contracts;
using Client.Application.Exceptions;
using Client.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using MediatR;

namespace Client.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;
    
    public DeleteClientCommandHandler(IClientRepository clientRepository, IMapper mapper, IEventBus eventBus)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }
    
    public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var clientToDelete = await _clientRepository.GetByIdAsync(request.Id);
        if (clientToDelete == null)
        {
            throw new NotFoundException(nameof(clientToDelete), request.Id);
        }
        
        await _clientRepository.DeleteAsync(clientToDelete);
        
        var clientDeletedEvent = new ClientDeletedEvent(request.Id);
        await _eventBus.PublishAsync(clientDeletedEvent);
        
        return Unit.Value;
    }
}