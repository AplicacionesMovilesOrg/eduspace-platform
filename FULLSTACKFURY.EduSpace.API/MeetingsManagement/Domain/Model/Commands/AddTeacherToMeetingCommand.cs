namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;

public record AddTeacherToMeetingCommand(string TeacherId, string MeetingId);