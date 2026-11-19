using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public static class GetRoomEndpoint
{
    public static void MapGetRoom(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/rooms/{id:guid}", async (
            Guid id,
            GetRoomHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(new GetRoomQuery(id), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToProblem();

            return Results.Ok(result.Value);
        }).WithTags("Rooms");
    }
}
