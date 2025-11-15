using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for Reservation aggregate
/// </summary>
public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(IMongoCollection<Reservation> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Remove a reservation asynchronously
    /// </summary>
    public async Task RemoveAsync(Reservation entity)
    {
        await base.RemoveAsync(entity.Id);
    }

    /// <summary>
    ///     Find all reservations by area ID
    /// </summary>
    public async Task<IEnumerable<Reservation>> FindAllByAreaIdAsync(string areaId)
    {
        return await FindAllAsync(r => r.AreaId.Identifier == areaId);
    }

    /// <summary>
    ///     Find all reservations by area ID, month, and day
    /// </summary>
    public async Task<IEnumerable<Reservation>> FindAllByAreaIdMonthAndDayAsync(string areaId, int month, int day)
    {
        return await FindAllAsync(r =>
            r.AreaId.Identifier == areaId &&
            r.ReservationDate.Start.Month == month &&
            r.ReservationDate.Start.Day == day);
    }
}