namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;

public interface IRExternalProfileService
{
    bool ValidateTeacherExistence(string teacherId);
    bool ValidateAdminIdExistence(string adminId);

    bool ValidateTeachersExistence(List<string> teacherIds);
}