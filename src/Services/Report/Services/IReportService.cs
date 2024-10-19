using Report.Service.Domain.Dto;

namespace Report.Service.Services;

public interface IReportService
{
    IEnumerable<ClientTransactionVm> GetTransactionsByClient(int clientId, DateTime startdate,
        DateTime endDate);
}