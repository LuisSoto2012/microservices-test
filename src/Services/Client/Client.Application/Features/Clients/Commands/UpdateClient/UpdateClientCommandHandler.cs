using AutoMapper;
using Client.Application.Contracts;
using Client.Application.Exceptions;
using MediatR;

namespace Client.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Unit>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public UpdateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var clientToUpdate = await _clientRepository.GetByIdAsync(request.Id);
        
        if (clientToUpdate == null)
        {
            throw new NotFoundException(nameof(clientToUpdate), request.Id);
        }

        _mapper.Map(request, clientToUpdate, typeof(UpdateClientCommand), typeof(Domain.Entities.Client));

        await _clientRepository.UpdateAsync(clientToUpdate);

        return Unit.Value;
    }
}