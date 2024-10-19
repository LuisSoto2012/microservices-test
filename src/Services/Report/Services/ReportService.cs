using Report.Service.Domain.Dto;
using Report.Service.InMemoryDatabase;

namespace Report.Service.Services;

public class ReportService : IReportService
{
    public IEnumerable<ClientTransactionVm> GetTransactionsByClient(int clientId, DateTime startDate, DateTime endDate)
    { 
        var transactions = InMemoryDb.Transactions
            .Where(t => t.ClientId == clientId && 
                        t.Date.Date >= startDate.Date && 
                        t.Date.Date <= endDate.Date)
            .Join(InMemoryDb.Clients,
                transaction => transaction.ClientId,
                client => client.ClientId,
                (transaction, client) => new ClientTransactionVm
                {
                    ClientName = client.ClientName,
                    Date = transaction.Date,
                    AccountNumber = transaction.AccountNumber,
                    AccountType = transaction.AccountType,
                    InitialBalance = transaction.Balance - transaction.Amount,
                    Amount = transaction.Amount,
                    Balance = transaction.Balance
                });

        return transactions.ToList();
    }
}