using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.SharedArea;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;

/// <summary>
///     Represents a shared area in the application.
/// </summary>
/// <remarks>
///     This class is used to represent a shared area in the application.
/// </remarks>
public class SharedArea
{
    /// <summary>
    ///     Default constructor for the shared area entity
    /// </summary>
    public SharedArea()
    {
    }

    /// <param name="name">
    ///     The name of the shared area
    /// </param>
    /// <param name="capacity">
    ///     The capacity of the shared area
    /// </param>
    /// <param name="description">
    ///     The description of the shared area
    /// </param>
    public SharedArea(string name, int capacity, string description) : this()
    {
        Name = name;
        Capacity = capacity;
        Description = description;
    }

    public SharedArea(CreateSharedAreaCommand command) : this()
    {
        Name = command.Name;
        Capacity = command.Capacity;
        Description = command.Description;
    }

    public SharedArea(UpdateSharedAreaCommand command) : this()
    {
        Id = command.Id;
        Name = command.Name;
        Capacity = command.Capacity;
        Description = command.Description;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;

    public void UpdateName(string name)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;
    }

    public void UpdateDescription(string description)
    {
        if (!string.IsNullOrEmpty(description))
            Description = description;
    }

    public void UpdateCapacity(int capacity)
    {
        if (capacity > 0)
            Capacity = capacity;
    }
}