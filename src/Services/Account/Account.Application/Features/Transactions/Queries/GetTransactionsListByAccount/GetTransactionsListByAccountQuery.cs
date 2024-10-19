using Account.Application.Features.Transactions.Queries.GetTransactionsList;
using MediatR;

namespace Account.Application.Features.Transactions.Queries.GetTransactionsListByAccount;

public class GetTransactionsListByAccountQuery : IRequest<List<TransactionsVm>>
{
    public int AccountId { get; set; }

    public GetTransactionsListByAccountQuery(int accountId)
    {
        AccountId = accountId;
    }
}