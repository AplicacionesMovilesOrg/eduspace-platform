using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.SharedArea;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.SharedArea;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Transform.SharedArea;

public static class UpdateSharedAreaCommandFromResourceAssembler
{
    public static UpdateSharedAreaCommand ToCommandFromResource(string Id, UpdateSharedAreaResource resource)
    {
        return new UpdateSharedAreaCommand(
            Id,
            resource.Name,
            resource.Capacity,
            resource.Description
        );
    }
}