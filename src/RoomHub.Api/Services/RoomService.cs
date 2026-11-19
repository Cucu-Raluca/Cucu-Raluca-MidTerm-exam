using RoomHub.Api.Domain;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Services;

public class RoomService(RoomRepository rooms)
{
    public Room Create(string? name, int capacity)
    {
        var result = Room.Create(name, capacity);
        if (result.IsFailure)
            throw new BusinessException(result.Error.Code, result.Error.Message, 400);

        var room = result.Value;
        rooms.Add(room);
        return room;
    }

    public Room Get(Guid id)
    {
        var room = rooms.Get(id);
        if (room is null)
            throw new BusinessException("RoomNotFound", "The room does not exist.", 404);

        return room;
    }
}
