using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Transform;

public static class UpdateMeetingCommandFromResourceAssembler
{
    public static UpdateMeetingCommand ToCommandFromResource(string meetingId, UpdateMeetingResource resource)
    {
        return new UpdateMeetingCommand(
            meetingId,
            resource.Title,
            resource.Description,
            resource.Date,
            resource.Start,
            resource.End,
            resource.AdministratorId,
            resource.ClassroomId);
    }
}