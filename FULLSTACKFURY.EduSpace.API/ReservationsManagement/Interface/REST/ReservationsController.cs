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
    [SwaggerResponse(201, "The category was created", typeof(ReservationResource))]
    public async Task<IActionResult> CreateReservation([FromRoute] string teacherId, [FromRoute] string areaId,
        [FromBody] CreateReservationResource resource)
    {
        var createReservationCommand =
            CreateReservationCommandFromResourceAssembler.ToCommandFromResource(areaId, teacherId, resource);
        var reservation = await reservationCommandService.Handle(createReservationCommand);

        if (reservation is null) return BadRequest();

        var reservationResource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        return Ok(reservationResource);
    }

    [HttpGet("[controller]")]
    public async Task<IActionResult> GetAllReservations()
    {
        var getAllReservationsQuery = new GetAllReservationsQuery();
        var reservations = await reservationQueryService.Handle(getAllReservationsQuery);
        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("areas/{areaId}/[controller]")]
    public async Task<IActionResult> GetAllReservationsByAreaId([FromRoute] string areaId)
    {
        var getAllReservationsByAreaIdQuery = new GetAllReservationsByAreaIdQuery(areaId);
        var reservations = await reservationQueryService.Handle(getAllReservationsByAreaIdQuery);

        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }
    //
    // [HttpGet("areas/{areaId:int}")]
    // public async Task<IActionResult> GetAllReservationsByAreaIdMonthAndDay(int areaId, [FromQuery] int month,
    //     [FromQuery] int day)
    // {
    //     var getAllReservationsByAreaIdMonthAndDayQuery = new GetAllReservationsByAreaIdAn
    // }
    //
}