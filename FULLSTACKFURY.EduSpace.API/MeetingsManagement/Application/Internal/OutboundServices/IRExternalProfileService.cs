namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;

public interface IRExternalProfileService
{
    Task<bool> ValidateTeacherExistence(string teacherId); 
    Task<bool> ValidateAdminIdExistence(string adminId); 
    Task<bool> ValidateTeachersExistence(List<string> teacherIds); 

}