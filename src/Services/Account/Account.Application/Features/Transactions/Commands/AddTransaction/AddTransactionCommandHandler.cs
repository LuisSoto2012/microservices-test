using Account.Application.Contracts;
using Account.Application.Exceptions;
using Account.Application.IntegrationEvents.Events;
using AutoMapper;
using EventBus.Abstractions;
using MediatR;

namespace Account.Application.Features.Transactions.Commands.AddTransaction;

public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public AddTransactionCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IMapper mapper, IEventBus eventBus)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }
    
    public async Task Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        // Get account
        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account == null)
        {
            throw new NotFoundException(nameof(account), request.AccountId);    
        }
        
        var transaction = _mapper.Map<Domain.Entities.Transaction>(request);
        
        // Validate if account has balance
        if (transaction.Type == "Retiro" && account.CurrentBalance < transaction.Amount)
        {
            throw new InsufficientBalanceException("Insufficient balance to perform the transaction.");
        }

        transaction.InitialBalance = account.CurrentBalance; // Previous account balance
        transaction.Balance =
            transaction.InitialBalance + (transaction.Type == "Deposito" ? transaction.Amount : -transaction.Amount); // Current balance
        
        await _transactionRepository.AddAsync(transaction);
        
        // Update new current balance in Account
        account.CurrentBalance = transaction.Balance;
        await _accountRepository.UpdateAsync(account);
        
        await _eventBus.PublishAsync(new TransactionCreatedEvent(account.ClientId, transaction.Date, account.Number, account.Type, transaction.InitialBalance, transaction.Amount, transaction.Balance));
    }
}