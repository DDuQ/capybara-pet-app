﻿using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddUserAchievement;

public class AssignUserAchievementCommandHandler : IRequestHandler<AssignUserAchievementCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAchievementRepository _achievementRepository;

    public AssignUserAchievementCommandHandler(IUserRepository userRepository, IAchievementRepository achievementRepository)
    {
        _userRepository = userRepository;
        _achievementRepository = achievementRepository;
    }


    public async Task<ErrorOr<Success>> Handle(AssignUserAchievementCommand request, CancellationToken cancellationToken)
    {
        var achievement = await _achievementRepository.GetByIdAsync(request.AchievementId);

        if (achievement is null)
        {
            return Error.NotFound(description: "Achievement does not exists.");
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User does not exists.");
        }

        var userAchievement = new UserAchievement(request.UserId, request.AchievementId);

        user.AssignUserAchievement(userAchievement);

        return Result.Success;
    }
}
