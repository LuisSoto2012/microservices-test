using System.Net;
using Account.Application.Exceptions;
using Account.Application.Features.Accounts.Commands.AddAccount;
using Account.Application.Features.Accounts.Commands.DeleteAccount;
using Account.Application.Features.Accounts.Commands.UpdateAccount;
using Account.Application.Features.Accounts.Queries.GetAccountsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpPost(Name = "AddAccount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> AddAccount([FromBody] AddAccountCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut(Name = "UpdateAccount")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateAccount([FromBody] UpdateAccountCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing your request." });
        }
    }
        
    [HttpDelete(Name = "DeleteAccount")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteAccount(int id)
    {
        try
        {
            var command = new DeleteAccountCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing your request." });
        }
    }
    
    [HttpGet(Name = "GetAccounts")]
    [ProducesResponseType(typeof(IEnumerable<AccountsVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<AccountsVm>>> GetAccounts()
    {
        var query = new GetAccountsListQuery();
        var accounts = await _mediator.Send(query);
        return Ok(accounts);
    }
}