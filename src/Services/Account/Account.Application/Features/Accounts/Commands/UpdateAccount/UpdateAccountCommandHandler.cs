using Account.Application.Contracts;
using Account.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Account.Application.Features.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public UpdateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var accountToUpdate = await _accountRepository.GetByIdAsync(request.Id);
        
        if (accountToUpdate == null)
        {
            throw new NotFoundException(nameof(accountToUpdate), request.Id);
        }

        _mapper.Map(request, accountToUpdate, typeof(UpdateAccountCommand), typeof(Domain.Entities.Account));

        await _accountRepository.UpdateAsync(accountToUpdate);

        return Unit.Value;
    }
}