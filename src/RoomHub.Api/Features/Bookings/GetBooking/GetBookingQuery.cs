using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public record GetBookingQuery(Guid Id) : IQuery<Result<GetBookingResponse>>;

public record GetBookingResponse(
    Guid Id,
    Guid RoomId,
    string BookedBy,
    DateTime StartUtc,
    DateTime EndUtc,
    int Attendees,
    string Status);
