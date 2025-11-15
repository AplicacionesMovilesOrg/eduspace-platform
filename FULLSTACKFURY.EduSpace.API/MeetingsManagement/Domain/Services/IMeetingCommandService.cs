using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Services;

public interface IMeetingCommandService
{
    Task<Meeting?> Handle(CreateMeetingCommand command);
    Task Handle(DeleteMeetingCommand command);
    Task<Meeting?> Handle(UpdateMeetingCommand command);
    Task Handle(AddTeacherToMeetingCommand command);
}