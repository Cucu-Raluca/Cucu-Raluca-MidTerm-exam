using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public record CreateRoomRequest(string? Name, int Capacity);

public static class CreateRoomEndpoint
{
    public static void MapCreateRoom(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/rooms", async (
            CreateRoomRequest request,
            CreateRoomHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(
                new CreateRoomCommand(request.Name, request.Capacity), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToProblem();

            return Results.Created($"/api/rooms/{result.Value.Id}", result.Value);
        }).WithTags("Rooms");
    }
}
