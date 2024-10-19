using Account.Application.Contracts;
using Account.Application.IntegrationEvents.Events;
using EventBus.Abstractions;

namespace Account.Application.IntegrationEvents.EventHandling;

public class ClientDeletedEventHandler : IIntegrationEventHandler<ClientDeletedEvent>
{
    private readonly IAccountRepository _accountRepository;

    public ClientDeletedEventHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public async Task Handle(ClientDeletedEvent @event)
    {
        var accountToDelete = await _accountRepository.GetByClientIdAsync(@event.ClientId);
        if (accountToDelete != null)
        {
            await _accountRepository.DeleteAsync(accountToDelete);
        }
    }
}