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
        if (!profilesContextFacade.ValidateTeacherProfileIdExistence(command.TeacherId))
            throw new ArgumentException("Teacher ID does not exist.");

        // Assuming there is a validation method for AreaId in ISpacesAndResourceManagementFacade
        // if (!spacesAndResourceManagementFacade.ValidateAreaIdExistence(command.AreaId))
        //     throw new ArgumentException("Area ID does not exist.");

        var reservation = new Reservation(command);

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