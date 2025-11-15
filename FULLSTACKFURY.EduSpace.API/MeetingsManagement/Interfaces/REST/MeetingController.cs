using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/")]
[Produces(MediaTypeNames.Application.Json)]
public class MeetingsController : ControllerBase
{
    private readonly IMeetingCommandService meetingCommandService;
    private readonly IMeetingQueryService meetingQueryService;

    public MeetingsController(IMeetingCommandService meetingCommandService, IMeetingQueryService meetingQueryService)
    {
        this.meetingCommandService = meetingCommandService;
        this.meetingQueryService = meetingQueryService;
    }

    [HttpPost("administrators/{administratorId}/classrooms/{classroomId}/meetings")]
    [SwaggerOperation(
        Summary = "Creates a meeting",
        Description = "Creates a new meeting with specified details",
        OperationId = "CreateMeeting"
    )]
    [SwaggerResponse(201, "The meeting was created", typeof(MeetingResource))]
    public async Task<IActionResult> CreateMeeting([FromRoute] string administratorId, [FromRoute] string classroomId,
        [FromBody] CreateMeetingResource resource)
    {
        var createMeetingCommand =
            CreateMeetingCommandFromResourceAssembler.ToCommandFromResource(administratorId, classroomId, resource);
        var meeting = await meetingCommandService.Handle(createMeetingCommand);
        if (meeting is null) return BadRequest("Failed to create meeting.");
        var meetingResource = MeetingResourceFromEntityAssembler.ToResourceFromEntity(meeting);
        return Ok(meetingResource);
    }

    [HttpGet("meetings")]
    [SwaggerOperation(
        Summary = "Gets all meetings",
        Description = "Retrieves a list of all meetings",
        OperationId = "GetAllMeetings"
    )]
    public async Task<IActionResult> GetAllMeetings()
    {
        var getAllMeetingsQuery = new GetAllMeetingsQuery();
        var meetings = await meetingQueryService.Handle(getAllMeetingsQuery);
        var resources = meetings.Select(MeetingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("meetings/{id}")]
    [SwaggerOperation(
        Summary = "Gets a meeting by id",
        Description = "Retrieves a specific meeting by its ID",
        OperationId = "GetMeetingById"
    )]
    [SwaggerResponse(200, "The meeting was retrieved", typeof(MeetingResource))]
    [SwaggerResponse(404, "The meeting was not found")]
    public async Task<IActionResult> GetMeetingById([FromRoute] string id)
    {
        var getMeetingByIdQuery = new GetMeetingByIdQuery(id);
        var meeting = await meetingQueryService.Handle(getMeetingByIdQuery);

        if (meeting is null) return NotFound();

        var meetingResource = MeetingResourceFromEntityAssembler.ToResourceFromEntity(meeting);
        return Ok(meetingResource);
    }

    [HttpPut("meetings/{id}")]
    [SwaggerOperation(
        Summary = "Updates a meeting",
        Description = "Updates a meeting by its ID with the provided details",
        OperationId = "UpdateMeeting"
    )]
    [SwaggerResponse(200, "The meeting was updated successfully", typeof(MeetingResource))]
    [SwaggerResponse(404, "The meeting was not found")]
    public async Task<IActionResult> UpdateMeeting([FromRoute] string id, [FromBody] UpdateMeetingResource resource)
    {
        try
        {
            // Map the resource to the UpdateMeetingCommand
            var updateMeetingCommand = UpdateMeetingCommandFromResourceAssembler.ToCommandFromResource(id, resource);

            // Handle the update command
            var updatedMeeting = await meetingCommandService.Handle(updateMeetingCommand);

            // If the meeting was not updated, return not found
            if (updatedMeeting is null)
                return NotFound(new { Message = "Meeting not found." });

            // Map the updated meeting entity to the resource
            var meetingResource = MeetingResourceFromEntityAssembler.ToResourceFromEntity(updatedMeeting);
            return Ok(meetingResource);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { ex.Message });
        }
    }

    [HttpDelete("meetings/{id}")]
    [SwaggerOperation(
        Summary = "Deletes a meeting",
        Description = "Deletes the meeting specified by its ID",
        OperationId = "DeleteMeeting"
    )]
    [SwaggerResponse(200, "The meeting was deleted successfully.")]
    [SwaggerResponse(404, "Meeting not found.")]
    public async Task<IActionResult> DeleteMeeting([FromRoute] string id)
    {
        try
        {
            var deleteMeetingCommand = new DeleteMeetingCommand(id);
            await meetingCommandService.Handle(deleteMeetingCommand);
            return Ok($"Meeting with ID {id} was deleted successfully.");
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }
}