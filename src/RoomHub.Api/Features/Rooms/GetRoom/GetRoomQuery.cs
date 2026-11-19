using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public record GetRoomQuery(Guid Id) : IQuery<Result<GetRoomResponse>>;

public record GetRoomResponse(Guid Id, string Name, int Capacity);
