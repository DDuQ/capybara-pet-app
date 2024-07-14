using CapybaraPetApp.Application.Achievements.Commands;
using CapybaraPetApp.Domain.AchievementAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Achievements;

[Route("api/[controller]")]
public class AchievementsController : ApiController
{
    private readonly ISender _sender;

    public AchievementsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAchievement(AchievementType achievementType)
    {
        var command = new CreateAchievementCommand(achievementType);

        var createAchievementResult = await _sender.Send(command);

        if (createAchievementResult.IsError)
        {
            return Problem(createAchievementResult.Errors);
        }

        return Ok(createAchievementResult.Value);
    }
}
