using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;

public class MeetingAudit
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("meeting_id")] public string MeetingId { get; set; }

    [BsonElement("action")] public string Action { get; set; }

    [BsonElement("action_performed_by")] public string ActionPerformedBy { get; set; }

    [BsonElement("created_at")] public DateTimeOffset? CreatedDate { get; set; }

    [BsonElement("updated_at")] public DateTimeOffset? UpdatedDate { get; set; }

    [BsonElement("previous_state")] public string? PreviousState { get; set; }

    [BsonElement("new_state")] public string? NewState { get; set; }
}