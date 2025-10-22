using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Repositories;

public interface IReservationRepository : IBaseRepository<Reservation>
{
    Task<IEnumerable<Reservation>> FindAllByAreaIdAsync(string areaId);

    Task<IEnumerable<Reservation>> FindAllByAreaIdMonthAndDayAsync(string areaId, int month, int day);
}