namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.OutboundServices;

public interface IExternalProfileService
{
   Task<bool> ValidateTeacherIdExistence(string teacherId);
}