using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.ValueObjects;

public record TeacherId
{
    public TeacherId(string teacherIdentifier)
    {
        TeacherIdentifier = teacherIdentifier;
    }

    public TeacherId()
    {
        TeacherIdentifier = string.Empty;
    }

    [BsonElement("teacher_identifier")] public string TeacherIdentifier { get; init; }
}