using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Resources;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/teachers-profiles")]
[Produces(MediaTypeNames.Application.Json)]
public class TeachersProfilesController(
    ITeacherProfileCommandService teacherProfileCommandService,
    ITeacherQueryService teacherQueryService,
    IAdminProfileQueryService adminProfileQueryService
) : ControllerBase
{
    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> CreateTeacherProfile([FromBody] CreateTeacherProfileResource resource)
    {
        var account = HttpContext.Items["Account"] as Account;
        if (account is null)
            return Unauthorized(new { message = "Authentication required" });

        var adminProfile = await adminProfileQueryService.Handle(
            new GetAdministratorProfileByAccountIdQuery(new AccountId(account.Id)));

        if (adminProfile is null)
            return Forbid("Only administrators can create teacher profiles");

        var command = CreateTeacherProfileCommandFromResourceAssembler.ToCommandFromResource(resource, adminProfile.Id);
        var teacherProfile = await teacherProfileCommandService.Handle(command);

        if (teacherProfile is null) return BadRequest();

        var response = TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity(teacherProfile);
        return Ok(response);
    }

    // ---------------------------------------------------------
    // READ ALL
    // ---------------------------------------------------------
    [HttpGet]
    public async Task<IActionResult> GetAllTeacherProfiles()
    {
        var teachers = await teacherQueryService.Handle(new GetAllTeachersProfileQuery());
        var resources = teachers.Select(TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    // ---------------------------------------------------------
    // READ BY ID
    // ---------------------------------------------------------
    [HttpGet("{teacherId}")]
    public async Task<IActionResult> GetTeacherProfileById([FromRoute] string teacherId)
    {
        var teacher = await teacherQueryService.Handle(new GetTeacherProfileByIdQuery(teacherId));
        if (teacher is null) return NotFound();

        var resource = TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity(teacher);
        return Ok(resource);
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    [HttpPut("{teacherId}")]
    public async Task<IActionResult> UpdateTeacherProfile(
        [FromRoute] string teacherId,
        [FromBody] UpdateTeacherProfileResource resource)
    {
        var command = UpdateTeacherProfileCommandFromResourceAssembler.ToCommand(teacherId, resource);

        var updatedTeacher = await teacherProfileCommandService.Handle(command);
        if (updatedTeacher is null) return NotFound();

        var response = TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity(updatedTeacher);
        return Ok(response);
    }

    // ---------------------------------------------------------
    // DELETE
    // ---------------------------------------------------------
    [HttpDelete("{teacherId}")]
    public async Task<IActionResult> DeleteTeacherProfile([FromRoute] string teacherId)
    {
        await teacherProfileCommandService.Handle(new DeleteTeacherProfileCommand(teacherId));
        return NoContent();
    }
}