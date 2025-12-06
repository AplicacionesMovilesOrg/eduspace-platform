using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.ValueObjects;

public record AreaId
{
    public AreaId(string areaIdentifier)
    {
        if (string.IsNullOrWhiteSpace(areaIdentifier)) throw new ArgumentException("Area Id cannot be empty");
        Identifier = areaIdentifier;
    }

    public AreaId()
    {
        Identifier = string.Empty;
    }

    [BsonElement("identifier")] public string Identifier { get; init; } = string.Empty;
}