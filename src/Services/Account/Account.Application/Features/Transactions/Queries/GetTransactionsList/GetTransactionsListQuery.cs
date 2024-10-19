using MediatR;

namespace Account.Application.Features.Transactions.Queries.GetTransactionsList;

public class GetTransactionsListQuery : IRequest<List<TransactionsVm>>
{
    public GetTransactionsListQuery()
    {
        
    }
}