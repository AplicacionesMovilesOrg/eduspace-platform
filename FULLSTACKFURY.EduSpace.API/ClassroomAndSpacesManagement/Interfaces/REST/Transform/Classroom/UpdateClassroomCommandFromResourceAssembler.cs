using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Classroom;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.Classroom;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Transform.Classroom;

public static class UpdateClassroomCommandFromResourceAssembler
{
    public static UpdateClassroomCommand ToCommandFromResource(string id, UpdateClassroomResource resource)
    {
        return new UpdateClassroomCommand(
            resource.Id,
            resource.Name,
            resource.Description,
            resource.TeacherId
        );
    }
}