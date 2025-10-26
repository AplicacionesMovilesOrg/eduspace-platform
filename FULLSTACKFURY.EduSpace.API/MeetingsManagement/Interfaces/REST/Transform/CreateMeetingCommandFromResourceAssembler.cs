using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Transform;

public class CreateMeetingCommandFromResourceAssembler
{
    public static CreateMeetingCommand ToCommandFromResource(string administratorId, string classroomId,
        CreateMeetingResource resource)
    {
        return new CreateMeetingCommand(
            resource.Title,
            resource.Description,
            resource.Date,
            resource.Start,
            resource.End,
            administratorId,
            classroomId);
    }
}