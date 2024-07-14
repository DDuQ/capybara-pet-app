using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Items.Queries.GetItem;

public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ErrorOr<Item>>
{
    private readonly IItemRepository _itemRepository;

    public GetItemQueryHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public Task<ErrorOr<Item>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        
        throw new NotImplementedException();
    }
}
