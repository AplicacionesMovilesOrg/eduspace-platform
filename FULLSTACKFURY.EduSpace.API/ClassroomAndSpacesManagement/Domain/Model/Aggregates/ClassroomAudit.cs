using MongoDB.Bson.Serialization.Attributes;
using DateTimeOffset = System.DateTimeOffset;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;

public class ClassroomAudit
{
    [BsonElement("created_date")] public DateTimeOffset? CreatedDate { get; set; }

    [BsonElement("updated_date")] public DateTimeOffset? UpdatedDate { get; set; }
}