using System.Net;
using Client.Application.Exceptions;
using Client.Application.Features.Clients.Commands.AddClient;
using Client.Application.Features.Clients.Commands.DeleteClient;
using Client.Application.Features.Clients.Commands.UpdateClient;
using Client.Application.Features.Clients.Queries.GetClientsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers;

[ApiController]
[Route("api/v1/clients")]
public class ClientController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpPost(Name = "AddClient")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> AddClient([FromBody] AddClientCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut(Name = "UpdateClient")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateClient([FromBody] UpdateClientCommand command)
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
        
    [HttpDelete(Name = "DeleteClient")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteClient(int id)
    {
        try
        {
            var command = new DeleteClientCommand() { Id = id };
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
    
    [HttpGet(Name = "GetClients")]
    [ProducesResponseType(typeof(IEnumerable<ClientsVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<ClientsVm>>> GetClients()
    {
        var query = new GetClientsListQuery();
        var clients = await _mediator.Send(query);
        return Ok(clients);
    }
}