using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Interface.REST.Resources;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Interface.REST;

[ApiController]
[Route("api/v1/")]
[Produces(MediaTypeNames.Application.Json)]
public class ReservationsController(
    IReservationCommandService reservationCommandService,
    IReservationQueryService reservationQueryService)
    : ControllerBase
{
    [HttpPost("teachers/{teacherId}/areas/{areaId}/reservations")]
    [SwaggerOperation(
        Summary = "Creates a reservation",
        Description = "Creates a reservation to a specific area",
        OperationId = "CreateReservation"
    )]
    [SwaggerResponse(201, "The reservation was created", typeof(ReservationResource))]
    [SwaggerResponse(400, "Bad request")]
    public async Task<IActionResult> CreateReservation([FromRoute] string teacherId, [FromRoute] string areaId,
        [FromBody] CreateReservationResource resource)
    {
        var createReservationCommand =
            CreateReservationCommandFromResourceAssembler.ToCommandFromResource(areaId, teacherId, resource);
        var reservation = await reservationCommandService.Handle(createReservationCommand);

        if (reservation is null) return BadRequest();

        var reservationResource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        return StatusCode(201, reservationResource);
    }

    [HttpGet("[controller]")]
    [SwaggerOperation(
        Summary = "Gets all reservations",
        Description = "Gets all reservations from the system",
        OperationId = "GetAllReservations"
    )]
    [SwaggerResponse(200, "Reservations retrieved successfully", typeof(IEnumerable<ReservationResource>))]

    public async Task<IActionResult> GetAllReservations()
    {
        var getAllReservationsQuery = new GetAllReservationsQuery();
        var reservations = await reservationQueryService.Handle(getAllReservationsQuery);
        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("areas/{areaId}/[controller]")]
    [SwaggerOperation(
        Summary = "Gets  reservations by area ID",
        Description = "Gets all reservations for a specific area",
        OperationId = "GetAllReservationsByAreaId"
    )]
    [SwaggerResponse(200, "Reservations retrieved successfully", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> GetAllReservationsByAreaId([FromRoute] string areaId)
    {
        var getAllReservationsByAreaIdQuery = new GetAllReservationsByAreaIdQuery(areaId);
        var reservations = await reservationQueryService.Handle(getAllReservationsByAreaIdQuery);

        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }
    
}