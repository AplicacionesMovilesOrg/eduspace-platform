using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Classroom;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.Classroom;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Transform.Classroom;

/// <summary>
///     Assembler class to transform CreateClassroomResource to CreateClassroomCommand
/// </summary>
public class CreateClassroomCommandFromResourceAssembler
{
    /// <summary>
    ///     Transform CreateClassroomResource to CreateClassroomCommand
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateClassroomResource" /> resource to transform
    /// </param>
    /// <returns>
    ///     The resulting <see cref="CreateClassroomCommand" /> command with the values from the resource
    /// </returns>
    public static CreateClassroomCommand ToCommandFromResource(string teacherId, CreateClassroomResource resource)
    {
        return new CreateClassroomCommand(resource.Name, resource.Description, teacherId);
    }
}