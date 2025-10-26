namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.SharedArea;

public record UpdateSharedAreaCommand(
    string Id,
    string Name,
    int Capacity,
    string Description
);