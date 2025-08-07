using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetItems;

public class GetItemsQueryHandler : IQueryHandler<GetItemsQuery, ErrorOr<List<UserItem>>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IUserRepository _userRepository;

    public GetItemsQueryHandler(IItemRepository itemRepository, IUserRepository userRepository)
    {
        _itemRepository = itemRepository;
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<List<UserItem>>> Handle(GetItemsQuery query, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        var items = await _itemRepository.GetItemsByUserIdAsync(user.Id);
        
        return items;
    }
}