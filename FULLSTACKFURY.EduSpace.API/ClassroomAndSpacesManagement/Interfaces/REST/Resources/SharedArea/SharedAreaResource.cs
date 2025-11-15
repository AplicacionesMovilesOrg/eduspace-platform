namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.REST.Resources.SharedArea;

/// <summary>
///     Represents the data required to create a new shared area.
/// </summary>
/// <param name="Name">
///     The name of the shared area
/// </param>
/// <param name="Capacity">
///     The capacity of the shared area
/// </param>
/// <param name="Description">
///     The description of the shared area
/// </param>
public record SharedAreaResource(string Id, string Name, int Capacity, string Description);