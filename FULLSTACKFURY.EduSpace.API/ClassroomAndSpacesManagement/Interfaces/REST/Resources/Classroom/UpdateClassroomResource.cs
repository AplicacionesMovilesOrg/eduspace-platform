namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.Classroom;

public record UpdateClassroomResource(
    string Id,
    string Name,
    string Description,
    string TeacherId
);