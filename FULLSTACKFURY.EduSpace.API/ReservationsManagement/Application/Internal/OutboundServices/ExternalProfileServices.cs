using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.OutboundServices;

public class ExternalProfileServices(IProfilesContextFacade contextFacade) : IExternalProfileService
{
    public async Task<bool> ValidateTeacherIdExistence(string teacherId)
    {
        return await contextFacade.ValidateTeacherProfileIdExistence(teacherId);
    }
}