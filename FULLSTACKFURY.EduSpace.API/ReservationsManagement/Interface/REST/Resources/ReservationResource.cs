namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Interface.REST.Resources;

public record ReservationResource(string Id, DateTime Start, DateTime End, string Title, string AreaId);