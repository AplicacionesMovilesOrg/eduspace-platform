using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<bool> ValidateTeacherProfileIdExistence(string teacherId);
    Task<bool> ValidateAdminProfileIdExistence(string adminId);
    Task<TeacherProfile?> GetTeacherProfileById(string teacherId);
    Task<IEnumerable<TeacherProfile>> GetTeacherProfilesByIds(IEnumerable<string> teacherIds);
}