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
}