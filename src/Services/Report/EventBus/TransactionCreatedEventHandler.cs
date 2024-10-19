using EventBus.Abstractions;
using Report.Service.Domain.Entities;
using Report.Service.Domain.Events;
using Report.Service.InMemoryDatabase;

namespace Report.Service.EventBus;

public class TransactionCreatedEventHandler : IIntegrationEventHandler<TransactionCreatedEvent>
{
    public Task Handle(TransactionCreatedEvent @event)
    {
  var transaction = new TransactionInfo
        {
            ClientId = @event.ClientId,
            Date = @event.Date,
            AccountNumber = @event.AccountNumber,
            AccountType = @event.AccountType,
            InitialBalance = @event.InitialBalance,
            Amount = @event.Amount,
            Balance = @event.Balance
        };
        InMemoryDb.Transactions.Add(transaction);
        return Task.CompletedTask;
    }
}