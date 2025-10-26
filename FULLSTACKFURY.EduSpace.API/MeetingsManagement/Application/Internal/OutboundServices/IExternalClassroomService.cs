namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;

public interface IExternalClassroomService
{
    bool ValidateClassroomId(string id);
}