using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.OutboundServices.ACL;

public class ExternalProfileService(IProfilesContextFacade profilesContextFacade) : IExternalProfileService
{
    public bool VerifyProfile(string teacherProfileId)
    {
        return profilesContextFacade.ValidateTeacherProfileIdExistence(teacherProfileId);
    }
}