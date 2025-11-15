namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Commands;

public record CreateReservationCommand(string Title, DateTime Start, DateTime End, string AreaId, string TeacherId);