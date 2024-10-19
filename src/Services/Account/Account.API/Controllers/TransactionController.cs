using System.Net;
using Account.Application.Exceptions;
using Account.Application.Features.Transactions.Commands.AddTransaction;
using Account.Application.Features.Transactions.Commands.DeleteTransaction;
using Account.Application.Features.Transactions.Commands.UpdateTransaction;
using Account.Application.Features.Transactions.Queries.GetTransactionsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers;

[ApiController]
[Route("api/v1/transactions")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpPost(Name = "AddTransaction")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> AddTransaction([FromBody] AddTransactionCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (InsufficientBalanceException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
    
    [HttpPut(Name = "UpdateTransaction")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateTransaction([FromBody] UpdateTransactionCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing your request." });
        }
    }
        
    [HttpDelete(Name = "DeleteTransaction")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteTransaction(int id)
    {
        try
        {
            var command = new DeleteTransactionCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing your request." });
        }
    }
    
    [HttpGet(Name = "GetTransactions")]
    [ProducesResponseType(typeof(IEnumerable<TransactionsVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<TransactionsVm>>> GetTransactions()
    {
        var query = new GetTransactionsListQuery();
        var transactions = await _mediator.Send(query);
        return Ok(transactions);
    }   
}