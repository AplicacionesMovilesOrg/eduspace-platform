using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Resource;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.Resource;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Transform.Resource;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/classrooms/{classroomId}/resources")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Classrooms / Resource")]
public class ResourceController : ControllerBase
{
    private readonly IResourceCommandService _resourceCommandService;
    private readonly IResourceQueryService _resourceQueryService;

    public ResourceController(IResourceCommandService resourceCommandService,
        IResourceQueryService resourceQueryService)
    {
        _resourceCommandService = resourceCommandService;
        _resourceQueryService = resourceQueryService;
    }

    /// <summary>
    ///     Creates a new resource within a specific classroom.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateResource([FromRoute] string classroomId,
        [FromBody] CreateResourceResource resource)
    {
        if (!ObjectId.TryParse(classroomId, out _))
            return BadRequest("Invalid classroom ID format");

        var command = CreateResourceCommandFromResourceAssembler.ToCommandFromResource(classroomId, resource);
        var newResource = await _resourceCommandService.Handle(command);
        if (newResource is null) return NotFound(new { message = "Classroom not found" });

        var resourceDto = ResourceResourceFromEntityAssembler.ToResourceFromEntity(newResource);

        return CreatedAtAction(nameof(GetResourceById),
            new { classroomId = newResource.ClassroomId.ToString(), resourceId = newResource.Id }, 
            resourceDto);
    }

    /// <summary>
    ///     Gets all resources for a specific classroom.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllResourcesByClassroomId([FromRoute] string classroomId)
    {
        if (!ObjectId.TryParse(classroomId, out _))
            return BadRequest("Invalid classroom ID format");

        var query = new GetAllResourcesByClassroomIdQuery(classroomId);
        var resources = await _resourceQueryService.Handle(query);
        var resourceDtos = resources.Select(ResourceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resourceDtos);
    }

    /// <summary>
    ///     Gets a specific resource by its ID from a specific classroom.
    /// </summary>
    [HttpGet("{resourceId}")]
    public async Task<IActionResult> GetResourceById([FromRoute] string classroomId, [FromRoute] string resourceId)
    {
        var query = new GetResourceByIdQuery(resourceId);
        var resource = await _resourceQueryService.Handle(query);

        if (resource == null) 
            return NotFound();
        
        if (resource.ClassroomId.ToString() != classroomId)
            return NotFound();

        var resourceDto = ResourceResourceFromEntityAssembler.ToResourceFromEntity(resource);
        return Ok(resourceDto);
    }

    /// <summary>
    ///     Updates an existing resource.
    /// </summary>
    [HttpPut("{resourceId}")]
    public async Task<IActionResult> UpdateResource(
        [FromRoute] string classroomId,
        [FromRoute] string resourceId,
        [FromBody] UpdateResourceResource resource)
    {
        var existingQuery = new GetResourceByIdQuery(resourceId);
        var existingResource = await _resourceQueryService.Handle(existingQuery);
        
        if (existingResource == null)
            return NotFound("Resource not found");
        
        if (existingResource.ClassroomId.ToString() != classroomId)
            return NotFound("Resource does not belong to the specified classroom");

        var command = UpdateResourceCommandFromResourceAssembler.ToCommandFromResource(resourceId, resource);
        var updatedResource = await _resourceCommandService.Handle(command);
        
        if (updatedResource == null) 
            return BadRequest("Could not update resource");

        var resourceDto = ResourceResourceFromEntityAssembler.ToResourceFromEntity(updatedResource);
        return Ok(resourceDto);
    }

    /// <summary>
    ///     Deletes a resource by its ID.
    /// </summary>
    [HttpDelete("{resourceId}")]
    public async Task<IActionResult> DeleteResource(
        [FromRoute] string classroomId,
        [FromRoute] string resourceId)
    {
        var query = new GetResourceByIdQuery(resourceId);
        var resource = await _resourceQueryService.Handle(query);
        
        if (resource == null)
            return NotFound("Resource not found");
        
        if (resource.ClassroomId.ToString() != classroomId)
            return NotFound("Resource does not belong to the specified classroom");

        var command = new DeleteResourceCommand(resourceId);
        await _resourceCommandService.Handle(command);
        return NoContent();
    }
}