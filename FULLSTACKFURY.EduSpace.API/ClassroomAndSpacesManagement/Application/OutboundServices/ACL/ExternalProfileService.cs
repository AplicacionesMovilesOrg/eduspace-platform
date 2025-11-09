using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.OutboundServices.ACL;

public class ExternalProfileService(IProfilesContextFacade profilesContextFacade) : IExternalProfileService
{
    public async Task<bool> VerifyProfile(string teacherProfileId)
    {
        return await profilesContextFacade.ValidateTeacherProfileIdExistence(teacherProfileId);
    }
}