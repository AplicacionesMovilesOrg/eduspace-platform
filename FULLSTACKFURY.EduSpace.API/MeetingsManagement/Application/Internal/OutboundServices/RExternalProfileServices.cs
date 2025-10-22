using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;

public class RExternalProfileServices(IProfilesContextFacade contextFacade) : IRExternalProfileService
{
    public bool ValidateTeacherExistence(string teacherId)
    {
        return contextFacade.ValidateTeacherProfileIdExistence(teacherId);
    }

    public bool ValidateAdminIdExistence(string adminid)
    {
        return contextFacade.ValidateAdminProfileIdExistence(adminid);
    }

    public bool ValidateTeachersExistence(List<string> teacherIds)
    {
        foreach (var teacherId in teacherIds)
            if (!ValidateTeacherExistence(teacherId))
                return false;

        return true;
    }
}