using Account.Application.Contracts;
using Account.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Account.Application.Features.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    
    public DeleteAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var accountToDelete = await _accountRepository.GetByIdAsync(request.Id);
        if (accountToDelete == null)
        {
            throw new NotFoundException(nameof(accountToDelete), request.Id);
        }
        await _accountRepository.DeleteAsync(accountToDelete);
        
        return Unit.Value;
    }
}