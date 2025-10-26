using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.ValueObjects;

public record TeacherId
{
    public TeacherId(string teacherIdentifier)
    {
        if (string.IsNullOrWhiteSpace(teacherIdentifier)) throw new ArgumentException("Teacher Id cannot be empty");
        TeacherIdentifier = teacherIdentifier;
    }

    public TeacherId()
    {
        TeacherIdentifier = string.Empty;
    }

    [BsonElement("teacher_identifier")] public string TeacherIdentifier { get; init; }
}