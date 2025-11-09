namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;

public interface IExternalClassroomService
{
    Task<bool> ValidateClassroomId(string id); 
}