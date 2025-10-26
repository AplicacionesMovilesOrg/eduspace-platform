namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Classroom;

public record UpdateClassroomCommand(
    string ClassroomId,
    string Name,
    string Description,
    string TeacherId
);