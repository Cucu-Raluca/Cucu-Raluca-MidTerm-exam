using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public static class GetBookingEndpoint
{
    public static void MapGetBooking(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/bookings/{id:guid}", async (
            Guid id,
            GetBookingHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(new GetBookingQuery(id), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToProblem();

            return Results.Ok(result.Value);
        }).WithTags("Bookings");
    }
}
