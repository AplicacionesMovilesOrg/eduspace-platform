using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Resource;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.Resource;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Transform.Resource;

public static class UpdateResourceCommandFromResourceAssembler
{
    public static UpdateResourceCommand ToCommandFromResource(string resourceId, UpdateResourceResource resource)
    {
        return new UpdateResourceCommand(
            resourceId,
            resource.Name,
            resource.KindOfResource,
            resource.ClassroomId
        );
    }
}