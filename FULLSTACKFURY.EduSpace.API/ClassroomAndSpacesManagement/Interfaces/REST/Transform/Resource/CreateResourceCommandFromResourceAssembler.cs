using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Resource;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.Resource;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Transform.Resource;

/// <summary>
///     Assembler class to transform CreateClassroomResource to CreateClassroomCommand
/// </summary>
public class CreateResourceCommandFromResourceAssembler
{
    public static CreateResourceCommand ToCommandFromResource(string classroomId, CreateResourceResource resource)
    {
        return new CreateResourceCommand(resource.Name, resource.KindOfResource, classroomId);
    }
}