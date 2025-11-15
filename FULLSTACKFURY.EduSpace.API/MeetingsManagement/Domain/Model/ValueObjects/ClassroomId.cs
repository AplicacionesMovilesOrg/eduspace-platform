using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.ValueObjects;

public record ClassroomId
{
    public ClassroomId(string classroomIdentifier)
    {
        ClassroomIdentifier = classroomIdentifier;
    }

    public ClassroomId()
    {
        ClassroomIdentifier = string.Empty;
    }

    [BsonElement("classroom_identifier")] public string ClassroomIdentifier { get; init; }
}