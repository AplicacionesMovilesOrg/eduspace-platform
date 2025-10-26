namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.OutboundServices;

public interface IExternalProfileService
{
    bool ValidateTeacherIdExistence(string teacherId);
}