using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/meetings/{meetingId}/teachers/{teacherId}")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Meetings")]
public class MeetingParticipantsController(IMeetingCommandService commandService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddTeacherToMeeting([FromRoute] string meetingId, [FromRoute] string teacherId)
    {
        var addTeacherToMeetingResource = new AddTeacherToMeetingResource(teacherId, meetingId);
        var addTeacherToMeetingCommand = AddTeacherToMeetingCommandFromResourceAssembler
            .ToCommandFromResource(addTeacherToMeetingResource);
        await commandService.Handle(addTeacherToMeetingCommand);
        return Ok("Teacher added to meeting.");
    }
}