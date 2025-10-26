namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;

public record CreateMeetingResource(
    string Title,
    string Description,
    DateOnly Date,
    TimeOnly Start,
    TimeOnly End
);