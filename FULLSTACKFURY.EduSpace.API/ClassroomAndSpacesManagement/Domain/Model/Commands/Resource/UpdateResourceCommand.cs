namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Resource;

public record UpdateResourceCommand(
    string Id,
    string Name,
    string KindOfResource,
    string ClassroomId
);