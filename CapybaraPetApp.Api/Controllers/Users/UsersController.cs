using CapybaraPetApp.Api.Controllers.Users.Requests;
using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Users.Commands.AssignCapybara;
using CapybaraPetApp.Application.Users.Commands.AssignItem;
using CapybaraPetApp.Application.Users.Commands.AssignUserAchievement;
using CapybaraPetApp.Application.Users.Commands.CreateUser;
using CapybaraPetApp.Application.Users.Commands.UseItem;
using CapybaraPetApp.Application.Users.Queries.GetCapybaras;
using CapybaraPetApp.Application.Users.Queries.GetUser;
using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Users;

public class UsersController : ApiController
{
    private readonly ICommandHandler<AdoptCapybaraCommand, ErrorOr<Success>> _adoptCapybaraCommand;
    private readonly ICommandHandler<UseItemCommand, ErrorOr<Success>> _useItemCommand;
    private readonly ICommandHandler<AssignItemCommand, ErrorOr<Success>> _assignItemCommand;
    private readonly ICommandHandler<UnlockUserAchievementCommand, ErrorOr<Success>> _assignUserAchievementCommand;
    private readonly ICommandHandler<CreateUserCommand, ErrorOr<Guid>> _createUserCommand;
    private readonly IQueryHandler<GetCapybarasQuery, ErrorOr<List<Capybara>>> _getCapybarasQuery;
    private readonly IQueryHandler<GetUserQuery, ErrorOr<User>> _getUserQuery;

    public UsersController(
        ICommandHandler<AdoptCapybaraCommand, ErrorOr<Success>> adoptCapybaraCommand,
        ICommandHandler<UseItemCommand, ErrorOr<Success>> useItemCommand,
        ICommandHandler<AssignItemCommand, ErrorOr<Success>> assignItemCommand,
        ICommandHandler<UnlockUserAchievementCommand, ErrorOr<Success>> assignUserAchievementCommand,
        ICommandHandler<CreateUserCommand, ErrorOr<Guid>> createUserCommand,
        IQueryHandler<GetCapybarasQuery, ErrorOr<List<Capybara>>> getCapybarasQuery,
        IQueryHandler<GetUserQuery, ErrorOr<User>> getUserQuery)
    {
        _adoptCapybaraCommand = adoptCapybaraCommand;
        _useItemCommand = useItemCommand;
        _assignItemCommand = assignItemCommand;
        _assignUserAchievementCommand = assignUserAchievementCommand;
        _createUserCommand = createUserCommand;
        _getCapybarasQuery = getCapybarasQuery;
        _getUserQuery = getUserQuery;
    }

    [HttpGet(APIEndpoints.User.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetUserQuery(id);

        var getUserResult = await _getUserQuery.Handle(query);

        if (getUserResult.IsError)
        {
            return Problem(getUserResult.Errors);
        }

        return Ok(getUserResult.Value);
    }

    [HttpPost(APIEndpoints.User.Create)]
    public async Task<IActionResult> Create(RegisterUserRequest createUserRequest)
    {
        var command = new CreateUserCommand(createUserRequest.Username,
                                            createUserRequest.Email,
                                            createUserRequest.Id);

        var createUserResult = await _createUserCommand.Handle(command);

        if (createUserResult.IsError)
        {
            return Problem(createUserResult.Errors);
        }

        return Ok(createUserResult.Value);
    }

    [HttpPost(APIEndpoints.User.UseItem)]
    public async Task<IActionResult> UseItem([FromRoute] Guid userId, [FromBody] UseItemRequest useItemRequest)
    {
        var command = new UseItemCommand(userId, useItemRequest.CapybaraId, useItemRequest.ItemId, useItemRequest.ItemQuantity);

        var useItemResult = await _useItemCommand.Handle(command);

        if (useItemResult.IsError)
        {
            return Problem(useItemResult.Errors);
        }

        return Ok(useItemResult.Value);
    }

    [HttpPost(APIEndpoints.User.AssignItem)]
    public async Task<IActionResult> AssignItem([FromRoute] Guid userId, [FromBody] AssignItemRequest assignItemRequest)
    {
        var command = new AssignItemCommand(userId, assignItemRequest.ItemId);

        var assignItemCommandResult = await _assignItemCommand.Handle(command);

        if (assignItemCommandResult.IsError)
        {
            return Problem(assignItemCommandResult.Errors);
        }

        return Ok(assignItemCommandResult.Value);
    }

    [HttpPost(APIEndpoints.User.UnlockAchievement)]
    public async Task<IActionResult> UnlockAchievement([FromRoute] Guid userId, [FromRoute] Guid achievementId)
    {
        var command = new UnlockUserAchievementCommand(userId, achievementId);

        var assignUserAchievementResult = await _assignUserAchievementCommand.Handle(command);

        if (assignUserAchievementResult.IsError)
        {
            return Problem(assignUserAchievementResult.Errors);
        }

        return Ok(assignUserAchievementResult.Value);
    }

    [HttpPost(APIEndpoints.User.AdoptCapybara)]
    public async Task<IActionResult> AdoptCapybara(Guid userId, Guid capybaraId)
    {
        var command = new AdoptCapybaraCommand(userId, capybaraId);

        var assignCapybaraResult = await _adoptCapybaraCommand.Handle(command);

        if (assignCapybaraResult.IsError)
        {
            return Problem(assignCapybaraResult.Errors);
        }

        return Ok(assignCapybaraResult.Value);
    }

    [HttpGet(APIEndpoints.User.GetCapybaras)]
    public async Task<IActionResult> GetCapybaras([FromRoute] Guid id)
    {
        var query = new GetCapybarasQuery(id);

        var getCapybarasResult = await _getCapybarasQuery.Handle(query);

        if (getCapybarasResult.IsError)
        {
            return Problem(getCapybarasResult.Errors);
        }

        return Ok(getCapybarasResult.Value);
    }
}