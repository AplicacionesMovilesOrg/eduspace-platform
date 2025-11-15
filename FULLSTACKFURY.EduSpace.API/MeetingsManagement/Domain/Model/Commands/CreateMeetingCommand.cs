namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;

public record CreateMeetingCommand(
    string Title,
    string Description,
    DateOnly Date,
    TimeOnly Start,
    TimeOnly End,
    string AdministratorId,
    string ClassroomId);