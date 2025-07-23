using CapybaraPetApp.Api.Controllers.Achievements.Requests;
using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Achievements.Commands;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Achievements;

public class AchievementsController : ApiController
{
    private readonly ICommandHandler<CreateAchievementCommand, ErrorOr<Guid>> _command;

    public AchievementsController(ICommandHandler<CreateAchievementCommand, ErrorOr<Guid>> command)
    {
        _command = command;
    }

    [HttpPost(APIEndpoints.Achievements.Create)]
    public async Task<IActionResult> CreateAchievement([FromBody] CreateAchievementRequest createAchievementRequest)
    {
        var command = new CreateAchievementCommand(
            createAchievementRequest.Title,
            createAchievementRequest.Description,
            createAchievementRequest.Points,
            createAchievementRequest.Rarity);

        var createAchievementResult = await _command.Handle(command);

        return createAchievementResult.IsError
            ? Problem(createAchievementResult.Errors)
            : Ok(createAchievementResult.Value);
    }
}
