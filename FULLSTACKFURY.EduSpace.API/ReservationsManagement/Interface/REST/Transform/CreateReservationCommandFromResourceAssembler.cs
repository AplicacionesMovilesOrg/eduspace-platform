using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Interface.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Interface.REST.Transform;

public static class CreateReservationCommandFromResourceAssembler
{
    public static CreateReservationCommand ToCommandFromResource(string areaId, string teacherId,
        CreateReservationResource resource)
    {
        return new CreateReservationCommand(resource.Title
            , resource.Start, resource.End
            , areaId, teacherId);
    }
}