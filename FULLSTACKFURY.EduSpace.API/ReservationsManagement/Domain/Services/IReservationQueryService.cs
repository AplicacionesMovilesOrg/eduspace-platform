using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Queries;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Services;

public interface IReservationQueryService
{
    Task<IEnumerable<Reservation>> Handle(GetAllReservationsQuery query);
    Task<IEnumerable<Reservation>> Handle(GetAllReservationsByAreaIdQuery query);
    Task<IEnumerable<Reservation>> Handle(GetReservationByAreaIdMonthAndDay query);
    Task<IEnumerable<Reservation>> Handle(GetAllReservationsByTeacherIdQuery query);
}