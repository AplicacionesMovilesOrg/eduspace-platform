using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Resource;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.Resource;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Transform.Resource;
using Microsoft.AspNetCore.Mvc;

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
    [HttpPost] // La ruta es simplemente la base: .../{classroomId}/resources
    public async Task<IActionResult> CreateResource([FromRoute] string classroomId,
        [FromBody] CreateResourceResource resource)
    {
        var command = CreateResourceCommandFromResourceAssembler.ToCommandFromResource(classroomId, resource);
        var newResource = await _resourceCommandService.Handle(command);
        if (newResource is null) return new NotFoundObjectResult(new { message = "Classroom not found" });

        var resourceDto = ResourceResourceFromEntityAssembler.ToResourceFromEntity(newResource);

        return CreatedAtAction(nameof(GetResourceById),
            new { classroomId = newResource.ClassroomId, resourceId = newResource.Id }, resourceDto);
    }

    /// <summary>
    ///     Gets all resources for a specific classroom.
    /// </summary>
    [HttpGet] // La ruta es: .../{classroomId}/resources
    public async Task<IActionResult> GetAllResourcesByClassroomId([FromRoute] string classroomId)
    {
        var query = new GetAllResourcesByClassroomIdQuery(classroomId);
        var resources = await _resourceQueryService.Handle(query);
        var resourceDtos = resources.Select(ResourceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resourceDtos);
    }

    /// <summary>
    ///     Gets a specific resource by its ID from a specific classroom.
    /// </summary>
    [HttpGet("{resourceId}")] // La ruta es: .../{classroomId}/resources/{resourceId}
    public async Task<IActionResult> GetResourceById([FromRoute] string classroomId, [FromRoute] string resourceId)
    {
        var query = new GetResourceByIdQuery(resourceId);
        var resource = await _resourceQueryService.Handle(query);

        // Verificación de seguridad: el recurso debe pertenecer al aula especificada.
        if (resource == null || resource.ClassroomId != classroomId) return NotFound();

        var resourceDto = ResourceResourceFromEntityAssembler.ToResourceFromEntity(resource);
        return Ok(resourceDto);
    }

    /// <summary>
    ///     Updates an existing resource.
    /// </summary>
    [HttpPut("{resourceId}")] // La ruta es: .../{classroomId}/resources/{resourceId}
    public async Task<IActionResult> UpdateResource([FromRoute] string resourceId,
        [FromBody] UpdateResourceResource resource)
    {
        var command = UpdateResourceCommandFromResourceAssembler.ToCommandFromResource(resourceId, resource);
        var updatedResource = await _resourceCommandService.Handle(command);
        if (updatedResource == null) return BadRequest("Could not update resource.");

        var resourceDto = ResourceResourceFromEntityAssembler.ToResourceFromEntity(updatedResource);
        return Ok(resourceDto);
    }

    /// <summary>
    ///     Deletes a resource by its ID.
    /// </summary>
    [HttpDelete("{resourceId}")] // La ruta es: .../{classroomId}/resources/{resourceId}
    public async Task<IActionResult> DeleteResource([FromRoute] string resourceId)
    {
        var command = new DeleteResourceCommand(resourceId);
        await _resourceCommandService.Handle(command);
        return NoContent(); // Respuesta 204 No Content es estándar para DELETE exitoso.
    }
}