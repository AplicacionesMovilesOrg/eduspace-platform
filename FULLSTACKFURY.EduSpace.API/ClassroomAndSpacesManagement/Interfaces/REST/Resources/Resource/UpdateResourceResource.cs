namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.Resource;

public record UpdateResourceResource(
    string Id,
    string Name,
    string KindOfResource,
    string ClassroomId
);