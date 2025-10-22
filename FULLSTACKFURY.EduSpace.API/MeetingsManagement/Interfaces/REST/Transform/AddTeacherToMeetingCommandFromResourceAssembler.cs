using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Transform;

public static class AddTeacherToMeetingCommandFromResourceAssembler
{
    public static AddTeacherToMeetingCommand ToCommandFromResource(AddTeacherToMeetingResource resource)
    {
        return new AddTeacherToMeetingCommand(resource.TeacherId, resource.MeetingId);
    }
}