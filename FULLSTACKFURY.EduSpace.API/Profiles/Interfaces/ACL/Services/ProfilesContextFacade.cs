using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL.Services;

public class ProfilesContextFacade(
    ITeacherProfileRepository teacherProfileRepository,
    IAdminProfileRepository adminProfileRepository) : IProfilesContextFacade
{
    public async Task<bool> ValidateTeacherProfileIdExistence(string teacherId)
    {
        return await teacherProfileRepository.ExistsByTeacherProfileId(teacherId);
    }

    public async Task<bool> ValidateAdminProfileIdExistence(string adminId)
    {
        return await adminProfileRepository.ExistsByAdminProfileId(adminId);
    }

    public async Task<TeacherProfile?> GetTeacherProfileById(string teacherId)
    {
        return await teacherProfileRepository.FindByIdAsync(teacherId);
    }

    public async Task<IEnumerable<TeacherProfile>> GetTeacherProfilesByIds(IEnumerable<string> teacherIds)
    {
        var teachers = new List<TeacherProfile>();
        foreach (var teacherId in teacherIds)
        {
            var teacher = await teacherProfileRepository.FindByIdAsync(teacherId);
            if (teacher != null) teachers.Add(teacher);
        }

        return teachers;
    }
}