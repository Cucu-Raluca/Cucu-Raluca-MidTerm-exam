using RoomHub.Api.Common;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public class GetBookingHandler(IBookingRepository bookings)
    : IQueryHandler<GetBookingQuery, Result<GetBookingResponse>>
{
    public async Task<Result<GetBookingResponse>> Handle(
        GetBookingQuery query, CancellationToken cancellationToken = default)
    {
        var booking = await bookings.GetByIdAsync(query.Id, cancellationToken);
        if (booking is null)
            return Error.NotFound("BookingNotFound", "The booking does not exist.");

        return new GetBookingResponse(
            booking.Id,
            booking.RoomId,
            booking.BookedBy,
            booking.StartUtc,
            booking.EndUtc,
            booking.Attendees,
            booking.Status.ToString());
    }
}
