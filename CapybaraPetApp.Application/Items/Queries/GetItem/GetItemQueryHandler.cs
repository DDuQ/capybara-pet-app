using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
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

    public Task<ErrorOr<Item>> Handle(GetItemQuery query, CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
    }
}
