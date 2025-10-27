using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Queries.GetItem;

public class GetItemQueryHandler : IQueryHandler<GetItemQuery, ErrorOr<Item>>
{
    private readonly IItemRepository _itemRepository;

    public GetItemQueryHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Item>> Handle(GetItemQuery query, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(query.ItemId);

        return item is not null ? item : ItemErrors.NotFound;
    }
}