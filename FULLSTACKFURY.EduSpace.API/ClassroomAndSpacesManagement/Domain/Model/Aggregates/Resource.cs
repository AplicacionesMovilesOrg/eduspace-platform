using System.ComponentModel.DataAnnotations;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Resource;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;

/// <summary>
///     Represents a resource in the application.
/// </summary>
/// <remarks>
///     A resource is a physical object that can be used in a classroom.
/// </remarks>
public class Resource
{
    /// <summary>
    ///     Default constructor for the classroom entity
    /// </summary>
    public Resource()
    {
    }

    /// <param name="name">
    ///     The name of the resource
    /// </param>
    /// <param name="kind_of_resource">
    ///     The kind of resource
    /// </param>
    /// <param name="classroomId">
    ///     The classroom id
    /// </param>
    public Resource(string name, string kindOfResource, string classroomId) : this()
    {
        Name = name;
        KindOfResource = kindOfResource;
        ClassroomId = classroomId;
    }

    public Resource(CreateResourceCommand command)
    {
        Name = command.Name;
        KindOfResource = command.KindOfResource;
        ClassroomId = command.ClassroomId;
    }

    public Resource(UpdateResourceCommand command)
    {
        Id = command.Id;
        Name = command.Name;
        KindOfResource = command.KindOfResource;
        ClassroomId = command.ClassroomId;
    }

    [Key]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; private set; }
    public string KindOfResource { get; private set; }
    public Classroom Classroom { get; internal set; }
    public string ClassroomId { get; private set; }

    public void UpdateName(string name)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;
    }

    public void UpdateKindOfResource(string kindOfResource)
    {
        if (!string.IsNullOrEmpty(kindOfResource))
            KindOfResource = kindOfResource;
    }

    public void UpdateClassroomId(string classroomId)
    {
        if (!string.IsNullOrEmpty(classroomId) && classroomId != ClassroomId)
            ClassroomId = classroomId;
    }
}