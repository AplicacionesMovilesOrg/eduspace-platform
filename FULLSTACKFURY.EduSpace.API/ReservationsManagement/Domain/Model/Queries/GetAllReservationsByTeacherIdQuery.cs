namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Queries;

/// <summary>
///     Query to get all reservations by teacher ID
/// </summary>
/// <param name="TeacherId">
///     The teacher ID to search for
/// </param>
public record GetAllReservationsByTeacherIdQuery(string TeacherId);
