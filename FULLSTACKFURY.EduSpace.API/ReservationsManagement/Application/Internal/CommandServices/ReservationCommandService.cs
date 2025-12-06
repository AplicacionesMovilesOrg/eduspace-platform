using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.CommandServices;

public class ReservationCommandService(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    IProfilesContextFacade profilesContextFacade,
    ISpacesAndResourceManagementFacade spacesAndResourceManagementFacade) : IReservationCommandService
{
    public async Task<Reservation?> Handle(CreateReservationCommand command)
    {
        // Validate teacher exists by account ID
        if (!await profilesContextFacade.ValidateTeacherAccountIdExistence(command.TeacherId))
            throw new ArgumentException("Teacher ID does not exist.");

        // Validate area exists
        if (!await spacesAndResourceManagementFacade.ValidateAreaIdExistence(command.AreaId))
            throw new ArgumentException("Area ID does not exist.");

        // Validate date range
        if (command.Start >= command.End)
            throw new ArgumentException("Start date must be before end date.");

        // Validate not in the past
        if (command.Start < DateTime.UtcNow)
            throw new ArgumentException("Cannot create reservations in the past.");

        // Validate maximum duration (e.g., 8 hours)
        var duration = command.End - command.Start;
        if (duration.TotalHours > 8)
            throw new ArgumentException("Reservation duration cannot exceed 8 hours.");

        var reservation = new Reservation(command);

        // Check for time conflicts
        var existingReservations = await reservationRepository.FindAllByAreaIdAsync(command.AreaId);
        if (!reservation.CanReserve(existingReservations))
            throw new InvalidOperationException("The selected time slot is already reserved.");

        await reservationRepository.AddAsync(reservation);
        await unitOfWork.CompleteAsync();

        return reservation;
    }

    public async Task Handle(DeleteReservationCommand command)
    {
        var reservation = await reservationRepository.FindByIdAsync(command.ReservationId);
        if (reservation == null) throw new Exception("The reservation does not exist");

        await reservationRepository.RemoveAsync(reservation);
        await unitOfWork.CompleteAsync();
    }
}