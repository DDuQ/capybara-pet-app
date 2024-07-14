using CapybaraPetApp.Application.Users.Commands.AddCapybara;
using CapybaraPetApp.Application.Users.Commands.AddItem;
using CapybaraPetApp.Application.Users.Commands.AddUserAchievement;
using CapybaraPetApp.Application.Users.Commands.CreateUser;
using CapybaraPetApp.Application.Users.Commands.UseItem;
using CapybaraPetApp.Application.Users.Queries.GetCapybaras;
using CapybaraPetApp.Application.Users.Queries.GetUser;
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

    [HttpPost("capybara")]
    public async Task<IActionResult> AssignCapybara(Guid userId, Guid capybaraId)
    {
        var command = new AssignCapybaraCommand(userId, capybaraId);

        var assignCapybaraResult = await _sender.Send(command);

        if (assignCapybaraResult.IsError)
        {
            return Problem(assignCapybaraResult.Errors);
        }

        return Ok(assignCapybaraResult.Value);
    }

    [HttpPost("use-item")]
    public async Task<IActionResult> UseItem(Guid userId, Guid capybaraId, Guid itemId, int amount)
    {
        var command = new UseItemCommand(userId, capybaraId, itemId, amount);

        var useItemResult = await _sender.Send(command);

        if (useItemResult.IsError)
        {
            return Problem(useItemResult.Errors);
        }

        return Ok(useItemResult.Value);
    }

    [HttpPost("user-item")]
    public async Task<IActionResult> AssignItem(Guid userId, Guid itemId)
    {
        var command = new AssignItemCommand(userId, itemId);

        var assignItemCommandResult = await _sender.Send(command);

        if (assignItemCommandResult.IsError)
        {
            return Problem(assignItemCommandResult.Errors);
        }

        return Ok(assignItemCommandResult.Value);
    }

    [HttpPost("user-achievement")]
    public async Task<IActionResult> AssignUserAchievement(Guid userId, Guid achievementId)
    {
        var command = new AssignUserAchievementCommand(userId, achievementId);

        var assignUserAchievementResult = await _sender.Send(command);

        if (assignUserAchievementResult.IsError)
        {
            return Problem(assignUserAchievementResult.Errors);
        }

        return Ok(assignUserAchievementResult.Value);
    }

    [HttpGet("capybaras")]
    public async Task<IActionResult> GetCapybarasByUserId(Guid id)
    {
        var query = new GetCapybarasQuery(id);

        var getCapybarasResult = await _sender.Send(query);

        if (getCapybarasResult.IsError)
        {
            return Problem(getCapybarasResult.Errors);
        }

        return Ok(getCapybarasResult.Value);
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

        var createUserResult = await _sender.Send(command);

        if (createUserResult.IsError)
        {
            return Problem(createUserResult.Errors);
        }

        return Ok(createUserResult.Value);
    }
}