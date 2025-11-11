using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class UserRepository(CapybaraPetAppDbContext dbContext, IDbConnectionFactory dbConnectionFactory) : Repository<User>(dbContext), IUserRepository
{
    private readonly DbSet<User> _users = dbContext.User;

    public async Task<bool> ExistsByEmail(string email)
    {
        return await _users.AnyAsync(x => x.Email == email);
    }

    public async Task<UserDto?> GetAllRelatedDataByIdAsync(Guid id)
    {
        using var dbConnection = await dbConnectionFactory.CreateConnection();
        const string userQuery = """
                             SELECT Id, Username, Email
                             FROM [User]
                             WHERE Id = @UserId;
                             """;
        
        const string achievementsQuery = """
                                         SELECT a.Title, a.Description, a.Points, ua.UnlockedAt
                                         FROM User_Achievement ua
                                         JOIN Achievement a ON a.Id = ua.AchievementId
                                         WHERE ua.UserId = @UserId; 
                                         """;
        const string capybarasQuery = """
                                      SELECT c.Name, 
                                             c.Stats_Happiness AS Happiness,
                                             c.Stats_Health AS Health,
                                             c.Stats_Energy AS Energy
                                      FROM User_Capybara uc
                                      JOIN Capybara c ON c.Id = uc.CapybaraId
                                      WHERE uc.UserId = @UserId;
                                      """;
        const string itemsQuery = """
                                  SELECT i.Name, 
                                         ui.Quantity AS Amount, 
                                         i.ItemDetail_ItemType AS ItemType, 
                                         i.ItemDetail_BonusEffect AS BonusEffect 
                                  FROM User_Item ui
                                  JOIN Item i ON i.Id = ui.ItemId
                                  WHERE ui.UserId = @UserId;
                                  """;
        
        var userResult = await dbConnection.QueryFirstOrDefaultAsync<UserDto>(userQuery, new { UserId = id });
        
        var capybarasResult = await dbConnection.QueryAsync<CapybaraDto, CapybaraStatsDto, CapybaraDto>(
            capybarasQuery,
            (capy, stats) =>
            {
                capy.Stats = stats;
                return capy;
            },
            param: new { UserId = id },
            splitOn: "Happiness");
        
        var itemsResult = await dbConnection.QueryAsync<ItemDto, ItemDetailDto, ItemDto>(
            itemsQuery, 
            (item, detail) =>
            {
                item.ItemDetail = detail;
                return item;
            },
            new { UserId = id },
            splitOn: "ItemType");
        
        var achievementsResult = await dbConnection.QueryAsync<AchievementDto>(achievementsQuery, new { UserId = id });
       
        userResult.Capybaras = capybarasResult.ToList();
        userResult.Items = itemsResult.ToList();
        userResult.Achievements = achievementsResult.ToList();
        
        return userResult;
    }

    public async Task<List<User>> GetAllAsync() => await _users.ToListAsync();
}