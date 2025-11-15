using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Commands;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Services;

public interface IReservationCommandService
{
    Task<Reservation?> Handle(CreateReservationCommand command);
    Task Handle(DeleteReservationCommand command);
}