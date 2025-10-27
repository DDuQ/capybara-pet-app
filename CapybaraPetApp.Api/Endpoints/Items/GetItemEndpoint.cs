using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Items.Queries.GetItem;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Items;

public static class GetItemEndpoint
{
    public const string Name = "GetItem";

    public static IEndpointRouteBuilder MapGetItem(this IEndpointRouteBuilder app)
    {
        app.MapGet(APIEndpoints.Item.Get, async (
                Guid id,
                IQueryHandler<GetItemQuery, ErrorOr<Item>> queryHandler) =>
            {
                var query = new GetItemQuery(id);

                var result = await queryHandler.Handle(query);

                return result.IsError
                    ? EndpointsExtensions.Problem(result.Errors)
                    : TypedResults.Ok(result.Value);
            })
            .WithName(Name);

        return app;
    }
}