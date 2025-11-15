using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Interface.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Interface.REST.Transform;

public static class ReservationResourceFromEntityAssembler
{
    public static ReservationResource ToResourceFromEntity(Reservation entity)
    {
        return new ReservationResource(
            entity.Id,
            entity.ReservationDate.Start,
            entity.ReservationDate.End,
            entity.Title,
            entity.AreaId.Identifier
        );
    }
}