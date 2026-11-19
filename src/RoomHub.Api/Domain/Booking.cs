using RoomHub.Api.Common;

namespace RoomHub.Api.Domain;

public enum BookingStatus
{
    Active,
    Cancelled
}

public class Booking
{
    private Booking() { }

    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    public static Result<Booking> Create(
        Guid roomId,
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        int roomCapacity,
        TimeProvider timeProvider)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
            return Result.Failure<Booking>(Error.Validation("InvalidBookedBy", "BookedBy is required."));

        if (endUtc <= startUtc)
            return Result.Failure<Booking>(Error.Validation("InvalidRange", "The end must be after the start."));

        if (attendees <= 0)
            return Result.Failure<Booking>(Error.Validation("InvalidAttendees", "Attendees must be positive."));

        if (attendees > roomCapacity)
            return Result.Failure<Booking>(Error.Validation("AttendeesExceedCapacity", "The room does not have enough seats."));

        var now = timeProvider.GetUtcNow().UtcDateTime;
        if (startUtc < now)
            return Result.Failure<Booking>(Error.Validation("PastBooking", "A booking cannot start in the past."));

        if (endUtc - startUtc > TimeSpan.FromHours(4))
            return Result.Failure<Booking>(Error.Validation("DurationExceeded", "The maximum duration of a booking is 4 hours."));

        return new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = roomId,
            BookedBy = bookedBy.Trim(),
            StartUtc = startUtc,
            EndUtc = endUtc,
            Attendees = attendees,
            Status = BookingStatus.Active
        };
    }

    public Result Cancel()
    {
        if (Status == BookingStatus.Cancelled)
            return Result.Failure(Error.Conflict("AlreadyCancelled", "Booking is already cancelled."));

        Status = BookingStatus.Cancelled;
        return Result.Success();
    }

    public bool OverlapsWith(DateTime start, DateTime end)
    {
        if (Status != BookingStatus.Active)
            return false;

        return start < EndUtc && StartUtc < end;
    }
}
