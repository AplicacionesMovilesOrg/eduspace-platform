namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Queries;

/// <summary>
///     Query to get a shared area by id.
/// </summary>
/// <param name="SharedAreaId">
///     The id of the shared area to get.
/// </param>
public record GetSharedAreaByIdQuery(string SharedAreaId);