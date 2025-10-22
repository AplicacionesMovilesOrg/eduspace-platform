using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.ValueObjects;

public record ResourceId
{
    public ResourceId(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Resource Id cannot be empty");
        Id = id;
    }

    public ResourceId()
    {
        Id = string.Empty;
    }

    [BsonElement("id")] public string Id { get; init; }
}