using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Classroom;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;

/// <summary>
///     Classroom aggregate root entity
/// </summary>
/// <remarks>
///     This class is used to represent a classroom in the application.
/// </remarks>
public class Classroom
{
    /// <summary>
    ///     Default constructor for the classroom entity
    /// </summary>
    /// <param name="name">
    ///     The name of the classroom
    /// </param>
    /// <param name="description">
    ///     The description of the classroom
    /// </param>
    /// <param name="teacherId">
    ///     The teacher id for the classroom
    /// </param>
    public Classroom()
    {
    }

    public Classroom(string name, string description, string teacherId)
    {
        Name = name;
        Description = description;
        TeacherId = new TeacherId(teacherId);
    }

    public Classroom(CreateClassroomCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        TeacherId = new TeacherId(command.TeacherId);
    }

    public Classroom(UpdateClassroomCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        TeacherId = new TeacherId(command.TeacherId);
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("name")] public string Name { get; private set; }

    [BsonElement("description")] public string Description { get; private set; }

    [BsonElement("teacher_id")] public TeacherId TeacherId { get; private set; }

    [BsonElement("resources")] public ICollection<Resource> Resources { get; private set; } = new List<Resource>();

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

    public void UpdateTeacherId(string? teacherId)
    {
        if (!string.IsNullOrWhiteSpace(teacherId))
            TeacherId = new TeacherId(teacherId);
    }
}