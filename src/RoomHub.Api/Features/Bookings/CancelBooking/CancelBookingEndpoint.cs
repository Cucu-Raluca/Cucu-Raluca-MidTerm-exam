using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public static class CancelBookingEndpoint
{
    public static void MapCancelBooking(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/bookings/{id:guid}/cancel", async (
            Guid id,
            CancelBookingHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(new CancelBookingCommand(id), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToProblem();

            return Results.NoContent();
        }).WithTags("Bookings");
    }
}
