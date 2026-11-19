using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain;

namespace RoomHub.Api.Infrastructure.Persistence;

public class BookingRepository(AppDbContext db) : IBookingRepository
{
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return db.Bookings.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public Task<List<Booking>> ListForRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
    {
        return db.Bookings.Where(b => b.RoomId == roomId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        await db.Bookings.AddAsync(booking, cancellationToken);
    }
}
