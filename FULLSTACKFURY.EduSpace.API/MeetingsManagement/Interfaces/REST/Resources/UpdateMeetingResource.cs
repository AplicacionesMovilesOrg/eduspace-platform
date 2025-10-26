namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;

public record UpdateMeetingResource(
    string MeetingId,
    string Title,
    string Description,
    DateOnly Date,
    TimeOnly Start,
    TimeOnly End,
    string AdministratorId,
    string ClassroomId
);