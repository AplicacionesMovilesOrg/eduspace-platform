using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL.Services;

public class ProfilesContextFacade(
    ITeacherProfileRepository teacherProfileRepository,
    IAdminProfileRepository adminProfileRepository) : IProfilesContextFacade
{
    public bool ValidateTeacherProfileIdExistence(string id)
    {
        return teacherProfileRepository.ExistsByTeacherProfileId(id);
    }

    public bool ValidateAdminProfileIdExistence(string id)
    {
        return adminProfileRepository.ExistsByAdminProfileId(id);
    }
}