using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;

public class ExternalClassroomServices(ISpacesAndResourceManagementFacade spacesFacade) : IExternalClassroomService
{
    public bool ValidateClassroomId(string id)
    {
        return spacesFacade.ValidateClassroomIdExistence(id);
    }
}