using MediatR;

namespace Account.Application.Features.Accounts.Queries.GetAccountsList;

public class GetAccountsListQuery : IRequest<List<AccountsVm>>
{
    public GetAccountsListQuery()
    {
        
    }
}