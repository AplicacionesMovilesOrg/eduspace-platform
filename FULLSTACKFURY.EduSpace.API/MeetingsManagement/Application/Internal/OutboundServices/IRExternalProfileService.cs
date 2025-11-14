using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;

public interface IRExternalProfileService
{
    Task<bool> ValidateTeacherExistence(string teacherId);
    Task<bool> ValidateAdminIdExistence(string adminId);
    Task<bool> ValidateTeachersExistence(List<string> teacherIds);
    Task<TeacherProfile?> GetTeacherProfileById(string teacherId);
    Task<IEnumerable<TeacherProfile>> GetTeacherProfilesByIds(IEnumerable<string> teacherIds);
}