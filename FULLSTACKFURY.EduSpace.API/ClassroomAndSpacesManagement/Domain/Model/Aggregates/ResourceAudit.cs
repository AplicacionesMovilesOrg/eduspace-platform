using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;

public class ResourceAudit
{
    [BsonElement("created_date")] public DateTimeOffset? CreatedDate { get; set; }

    [BsonElement("updated_date")] public DateTimeOffset? UpdatedDate { get; set; }
}