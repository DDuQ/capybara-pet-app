using CapybaraPetApp.Application.Users.Commands.AddCapybara;
using CapybaraPetApp.Application.Users.Commands.AddInteraction;
using CapybaraPetApp.Application.Users.Commands.CreateUser;
using CapybaraPetApp.Application.Users.Queries.GetCapybaras;
using CapybaraPetApp.Application.Users.Queries.GetUser;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Users;

[Route("api/[controller]")]
public class UsersController : ApiController
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var query = new GetUserQuery(id);

        var getUserResult = await _sender.Send(query);

        if (getUserResult.IsError)
        {
            return Problem(getUserResult.Errors);
        }

        return Ok(getUserResult.Value);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest createUserRequest)
    {
        var command = new CreateUserCommand(createUserRequest.Username,
                                            createUserRequest.Email,
                                            createUserRequest.Id);

        var result = await _sender.Send(command);

        if (result.IsError)
        {
            return BadRequest(result);
        }

        return Ok(result.Value);
    }

    [HttpPost("interaction")]
    public async Task<IActionResult> AddInteraction(InteractionDetail interactionDetail, Guid userId, Guid capybaraId)
    {

        var command = new AddInteractionCommand(userId, capybaraId, interactionDetail);

        var addInteractionResult = await _sender.Send(command);

        if (addInteractionResult.IsError)
        {
            return BadRequest(addInteractionResult.Errors);
        }

        return Ok(addInteractionResult.Value);
    }

    [HttpPost("capybara")]
    public async Task<IActionResult> AddCapybara(Guid userId, Guid capybaraId)
    {
        var command = new AddCapybaraCommand(userId, capybaraId);

        var addCapybaraResult = await _sender.Send(command);

        if (addCapybaraResult.IsError)
        {
            return BadRequest(addCapybaraResult.Errors);
        }

        return Ok(addCapybaraResult.Value);
    }

    [HttpGet("capybaras")]
    public async Task<IActionResult> GetCapybarasByUserId(Guid id)
    {
        var command = new GetCapybarasQuery(id);

        var getCapybarasResult = await _sender.Send(command);

        if (getCapybarasResult.IsError)
        {
            return Problem(getCapybarasResult.Errors);
        }

        return Ok(getCapybarasResult.Value);
    }
}
