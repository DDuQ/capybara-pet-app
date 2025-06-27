using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Achievements.Commands;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Achievements;

[Route("api/[controller]")]
public class AchievementsController : ApiController
{
    private readonly ICommandHandler<CreateAchievementCommand, ErrorOr<Achievement>> _command;

    public AchievementsController(ICommandHandler<CreateAchievementCommand, ErrorOr<Achievement>> command)
    {
        _command = command;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAchievement(AchievementType achievementType)
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
