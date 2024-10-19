using Account.Application.Contracts;
using AutoMapper;
using MediatR;

namespace Account.Application.Features.Accounts.Queries.GetAccountsList;

public class GetAccountsListQueryHandler : IRequestHandler<GetAccountsListQuery, List<AccountsVm>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    
    public GetAccountsListQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<List<AccountsVm>> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
    {
        var accountList = await _accountRepository.GetAllAsync();
        return _mapper.Map<List<AccountsVm>>(accountList);
    }
}