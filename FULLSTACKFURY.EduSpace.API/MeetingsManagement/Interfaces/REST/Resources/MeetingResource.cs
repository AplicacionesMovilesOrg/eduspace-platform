using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.ValueObjects;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;

public record MeetingResource(
    string MeetingId,
    string Title,
    string Description,
    DateOnly Date,
    TimeOnly Start,
    TimeOnly End,
    AdministratorId AdministratorId,
    ClassroomId ClassroomId,
    IEnumerable<TeacherResource> Teachers
);

public record TeacherResource(
    string Id,
    string FirstName,
    string LastName
);