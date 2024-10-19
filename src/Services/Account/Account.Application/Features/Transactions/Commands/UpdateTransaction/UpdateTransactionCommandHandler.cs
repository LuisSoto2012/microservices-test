using Account.Application.Contracts;
using Account.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Account.Application.Features.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Unit>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Unit> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transactionToUpdate = await _transactionRepository.GetByIdAsync(request.Id);
        
        if (transactionToUpdate == null)
        {
            throw new NotFoundException(nameof(transactionToUpdate), request.Id);
        }

        _mapper.Map(request, transactionToUpdate, typeof(UpdateTransactionCommand), typeof(Domain.Entities.Transaction));

        await _transactionRepository.UpdateAsync(transactionToUpdate);

        return Unit.Value;
    }
}