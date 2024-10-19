using Account.Application.Contracts;
using Account.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Account.Application.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Unit>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    
    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transactionToDelete = await _transactionRepository.GetByIdAsync(request.Id);
        if (transactionToDelete == null)
        {
            throw new NotFoundException(nameof(transactionToDelete), request.Id);
        }
        
        await _transactionRepository.DeleteAsync(transactionToDelete);

        return Unit.Value;
    }
}