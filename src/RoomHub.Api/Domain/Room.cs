using RoomHub.Api.Common;

namespace RoomHub.Api.Domain;

public class Room
{
    private Room() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Capacity { get; private set; }

    public static Result<Room> Create(string? name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Room>(Error.Validation("InvalidName", "Room name is required."));

        if (capacity <= 0)
            return Result.Failure<Room>(Error.Validation("InvalidCapacity", "Capacity must be positive."));

        return new Room
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Capacity = capacity
        };
    }
}
