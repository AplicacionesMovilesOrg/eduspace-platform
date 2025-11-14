using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;

public class RExternalProfileServices(IProfilesContextFacade contextFacade) : IRExternalProfileService
{
    public async Task<bool> ValidateTeacherExistence(string teacherId)
    {
        return await contextFacade.ValidateTeacherProfileIdExistence(teacherId);
    }

    public async Task<bool> ValidateAdminIdExistence(string adminid)
    {
        return await contextFacade.ValidateAdminProfileIdExistence(adminid);
    }

    public async Task<bool> ValidateTeachersExistence(List<string> teacherIds)
    {
        foreach (var teacherId in teacherIds)
        {
            if (!await ValidateTeacherExistence(teacherId))
                return false;
        }

        return true;
    }

    public async Task<TeacherProfile?> GetTeacherProfileById(string teacherId)
    {
        return await contextFacade.GetTeacherProfileById(teacherId);
    }

    public async Task<IEnumerable<TeacherProfile>> GetTeacherProfilesByIds(IEnumerable<string> teacherIds)
    {
        return await contextFacade.GetTeacherProfilesByIds(teacherIds);
    }
}