using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.OutboundServices;

public class ExternalProfileServices(IProfilesContextFacade contextFacade) : IExternalProfileService
{
    public bool ValidateTeacherIdExistence(string teacherId)
    {
        return contextFacade.ValidateTeacherProfileIdExistence(teacherId);
    }
}