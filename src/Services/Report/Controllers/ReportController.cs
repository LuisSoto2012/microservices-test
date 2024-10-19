using System.Net;
using Microsoft.AspNetCore.Mvc;
using Report.Service.Domain.Dto;
using Report.Service.Services;

namespace Report.Service.Controllers;

[ApiController]
[Route("api/v1/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("client-transactions", Name = "GetClientTransactions")]
    [ProducesResponseType(typeof(IEnumerable<ClientTransactionVm>), (int)HttpStatusCode.OK)]
    public IActionResult GetTransactionsByClient([FromQuery]int clientId, [FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
    {
        var transactions = _reportService.GetTransactionsByClient(clientId, startDate, endDate);
        return Ok(transactions);
    }
}