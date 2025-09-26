using CapybaraPetApp.Api.Endpoints.Items.Requests;
using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Items.Commands.CreateItem;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Items;

public static class CreateItemEndpoint
{
    private const string Name = "CreateItem";

    public static IEndpointRouteBuilder MapCreateItem(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.Item.Create, async (
                CreateItemRequest createItemRequest,
                ICommandHandler<CreateItemCommand, ErrorOr<Item>> commandHandler) =>
            {
                var command = new CreateItemCommand(createItemRequest.Name, createItemRequest.Quantity,
                    new ItemDetail(createItemRequest.ItemType));

                var result = await commandHandler.Handle(command);

                return result.IsError
                    ? EndpointsExtensions.Problem(result.Errors)
                    : TypedResults.CreatedAtRoute(result.Value, GetItemEndpoint.Name, new { id = result.Value.Id });
            })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}