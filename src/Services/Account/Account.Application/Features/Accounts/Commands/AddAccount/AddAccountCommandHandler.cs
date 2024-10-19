using Account.Application.Contracts;
using AutoMapper;
using MediatR;

namespace Account.Application.Features.Accounts.Commands.AddAccount;

public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, Domain.Entities.Account>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public AddAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Domain.Entities.Account> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<Domain.Entities.Account>(request);
        
        var newAccount = await _accountRepository.AddAsync(account);

        return newAccount;
    }
}