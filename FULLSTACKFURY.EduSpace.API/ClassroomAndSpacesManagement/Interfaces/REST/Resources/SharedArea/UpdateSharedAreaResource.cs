namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.SharedArea;

public record UpdateSharedAreaResource(
    string Id,
    string Name,
    int Capacity,
    string Description
);