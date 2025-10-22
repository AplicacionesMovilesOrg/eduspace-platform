using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.ValueObjects;

public record AdministratorId
{
    public AdministratorId(string administratorIdentifier)
    {
        AdministratorIdentifier = administratorIdentifier;
    }

    public AdministratorId()
    {
        AdministratorIdentifier = string.Empty;
    }

    [BsonElement("administrator_identifier")]
    public string AdministratorIdentifier { get; init; }
}