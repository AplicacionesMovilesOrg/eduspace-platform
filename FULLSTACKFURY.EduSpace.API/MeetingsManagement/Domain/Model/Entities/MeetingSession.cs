using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Entities;

public class MeetingSession
{
    public MeetingSession()
    {
    }

    public MeetingSession(string teacherId, string meetingId)
    {
        TeacherId = teacherId;
        MeetingId = meetingId;
    }

    [BsonElement("meeting_id")] public string MeetingId { get; set; }

    [BsonElement("teacher_id")] public string TeacherId { get; set; }

    [BsonIgnore] public Meeting Meeting { get; set; }

    [BsonIgnore] public TeacherProfile Teacher { get; set; }
}