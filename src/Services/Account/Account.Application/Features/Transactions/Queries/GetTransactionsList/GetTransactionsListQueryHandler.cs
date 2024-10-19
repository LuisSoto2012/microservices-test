using Account.Application.Contracts;
using AutoMapper;
using MediatR;

namespace Account.Application.Features.Transactions.Queries.GetTransactionsList;

public class GetTransactionsListQueryHandler : IRequestHandler<GetTransactionsListQuery, List<TransactionsVm>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    
    public GetTransactionsListQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<List<TransactionsVm>> Handle(GetTransactionsListQuery request, CancellationToken cancellationToken)
    {
        var accountList = await _transactionRepository.GetAllAsync();
        return _mapper.Map<List<TransactionsVm>>(accountList);
    }
}