using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Items.Commands.CreateItem;
using CapybaraPetApp.Application.Items.Queries.GetItem;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraPetApp.Api.Controllers.Items;

public class ItemsController : ApiController
{
    private readonly IQueryHandler<GetItemQuery, ErrorOr<Item>> _getItemQuery;
    private readonly ICommandHandler<CreateItemCommand, ErrorOr<Guid>> _createItemCommand;

    public ItemsController(IQueryHandler<GetItemQuery, ErrorOr<Item>> getItemQuery, ICommandHandler<CreateItemCommand, ErrorOr<Guid>> createItemCommand)
    {
        _getItemQuery = getItemQuery;
        _createItemCommand = createItemCommand;
    }

    [HttpPost(APIEndpoints.Item.Create)]
    public async Task<IActionResult> CreateItem(string name, ItemType itemType, int quantity, int bonusEffect)
    {
        var command = new CreateItemCommand(name, new ItemDetail(itemType, quantity, bonusEffect));
        
        var result = await _createItemCommand.Handle(command);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return CreatedAtAction(nameof(CreateItem), new { id = result.Value }, result.Value);
    }
}