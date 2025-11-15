namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;

public record UpdateMeetingCommand(
    string MeetingId,
    string Title,
    string Description,
    DateOnly Date,
    TimeOnly Start,
    TimeOnly End,
    string AdministratorId,
    string ClassroomId);