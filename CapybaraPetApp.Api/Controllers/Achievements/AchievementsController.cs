using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Achievements.Commands;
using CapybaraPetApp.Domain.AchievementAggregate;
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
    public async Task<IActionResult> CreateAchievement([FromBody] AchievementType achievementType)
    {
        var command = new CreateAchievementCommand(achievementType);

        var createAchievementResult = await _command.Handle(command);

        if (createAchievementResult.IsError)
        {
            return Problem(createAchievementResult.Errors);
        }

        return Ok(createAchievementResult.Value);
    }
}
