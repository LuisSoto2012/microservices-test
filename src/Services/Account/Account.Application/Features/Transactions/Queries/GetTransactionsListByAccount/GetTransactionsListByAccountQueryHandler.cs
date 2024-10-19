using Account.Application.Contracts;
using Account.Application.Features.Accounts.Queries.GetAccountsList;
using Account.Application.Features.Transactions.Queries.GetTransactionsList;
using AutoMapper;
using MediatR;

namespace Account.Application.Features.Transactions.Queries.GetTransactionsListByAccount;

public class GetTransactionsListByAccountQueryHandler : IRequestHandler<GetTransactionsListByAccountQuery, List<TransactionsVm>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    
    public GetTransactionsListByAccountQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<List<TransactionsVm>> Handle(GetTransactionsListByAccountQuery request, CancellationToken cancellationToken)
    {
        var accountList = await _transactionRepository.GetTransactionsByAccountAsync(request.AccountId);
        return _mapper.Map<List<TransactionsVm>>(accountList);
    }
}