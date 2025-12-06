using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Interface.REST.Resources;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Interface.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ReportsController(IReportCommandService reportCommandService, IReportQueryService reportQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a report",
        Description = "Creates a report for a specific resource",
        OperationId = "CreateReport"
    )]
    [SwaggerResponse(201, "The report was created", typeof(ReportResource))]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportResource resource)
    {
        var createReportCommand = CreateReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var report = await reportCommandService.Handle(createReportCommand);

        if (report is null) return BadRequest();

        var reportResource = ReportResourceFromEntityAssembler.ToResourceFromEntity(report);
        return Ok(reportResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
        var getAllReportsQuery = new GetAllReportsQuery();
        var reports = await reportQueryService.Handle(getAllReportsQuery);
        var resources = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("resources/{resourceId}")]
    public async Task<IActionResult> GetAllReportsByResourceId([FromRoute] string resourceId)
    {
        var getAllReportsByResourceIdQuery = new GetAllReportsByResourceIdQuery(resourceId);
        var reports = await reportQueryService.Handle(getAllReportsByResourceIdQuery);

        var resources = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("teachers/{teacherId}")]
    [SwaggerOperation(
        Summary = "Gets all reports by teacher ID",
        Description = "Gets all reports for resources that belong to classrooms assigned to a specific teacher",
        OperationId = "GetAllReportsByTeacherId"
    )]
    [SwaggerResponse(200, "The reports were retrieved successfully", typeof(IEnumerable<ReportResource>))]
    public async Task<IActionResult> GetAllReportsByTeacherId([FromRoute] string teacherId)
    {
        var getAllReportsByTeacherIdQuery = new GetAllReportsByTeacherIdQuery(teacherId);
        var reports = await reportQueryService.Handle(getAllReportsByTeacherIdQuery);

        var resources = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }
}