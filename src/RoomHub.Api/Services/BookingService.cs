using RoomHub.Api.Domain;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Services;

public class BookingService(RoomRepository rooms, BookingRepository bookings, TimeProvider timeProvider)
{
    public Booking Create(Guid roomId, string? bookedBy, DateTime startUtc, DateTime endUtc, int attendees)
    {
        var room = rooms.Get(roomId);
        if (room is null)
            throw new BusinessException("RoomNotFound", "The room does not exist.", 404);

        var result = Booking.Create(roomId, bookedBy, startUtc, endUtc, attendees, room.Capacity, timeProvider);
        if (result.IsFailure)
            throw new BusinessException(result.Error.Code, result.Error.Message, 400);

        foreach (var existing in bookings.ListForRoom(roomId))
        {
            if (existing.OverlapsWith(startUtc, endUtc))
                throw new BusinessException("RoomOccupied", "The room is occupied in this interval.", 409);
        }

        var booking = result.Value;
        bookings.Add(booking);
        return booking;
    }

    public Booking Get(Guid id)
    {
        var booking = bookings.Get(id);
        if (booking is null)
            throw new BusinessException("BookingNotFound", "The booking does not exist.", 404);

        return booking;
    }

    public void Cancel(Guid id)
    {
        var booking = Get(id);
        var cancelResult = booking.Cancel();
        if (cancelResult.IsFailure)
            throw new BusinessException(cancelResult.Error.Code, cancelResult.Error.Message, 409);
    }
}
