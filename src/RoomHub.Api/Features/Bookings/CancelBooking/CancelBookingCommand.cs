using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public record CancelBookingCommand(Guid Id) : ICommand;
