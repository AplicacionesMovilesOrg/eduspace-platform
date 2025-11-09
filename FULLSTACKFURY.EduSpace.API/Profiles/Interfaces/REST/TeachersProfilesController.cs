using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Queries;
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
    IAdminProfileQueryService adminProfileQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTeacherProfile([FromBody] CreateTeacherProfileResource resource)
    {
        // Extract the authenticated account from the request context
        var account = HttpContext.Items["Account"] as Account;
        if (account is null)
            return Unauthorized(new { message = "Authentication required" });

        // Get the administrator profile associated with the authenticated account
        var adminProfile = await adminProfileQueryService.Handle(new GetAdministratorProfileByAccountIdQuery(new Domain.Model.ValueObjects.AccountId(account.Id)));
        if (adminProfile is null)
            return Forbid("Only administrators can create teacher profiles");

        // Create the teacher profile with the administrator ID from the authenticated user
        var createProfileCommand = CreateTeacherProfileCommandFromResourceAssembler.ToCommandFromResource(resource, adminProfile.Id);
        var teacherProfile = await teacherProfileCommandService.Handle(createProfileCommand);
        if (teacherProfile is null) return BadRequest();
        var teacherProfileResource = TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity(teacherProfile);
        return Ok(teacherProfileResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTeacherProfiles()
    {
        var teacherProfiles = await teacherQueryService.Handle(new GetAllTeachersProfileQuery());
        var teacherResources
            = teacherProfiles.Select(TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(teacherResources);
    }

    [HttpGet("{teacherId}")]
    public async Task<IActionResult> GetTeacherProfileById([FromRoute] string teacherId)
    {
        var teacherProfile = await teacherQueryService.Handle(new GetTeacherProfileByIdQuery(teacherId));
        if (teacherProfile is null) return NotFound();
        var teacherResource = TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity(teacherProfile);
        return Ok(teacherResource);
    }
}