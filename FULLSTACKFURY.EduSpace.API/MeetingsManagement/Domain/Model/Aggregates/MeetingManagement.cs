using System.ComponentModel.DataAnnotations.Schema;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Entities;
using MongoDB.Bson.Serialization.Attributes;
using TeacherId = FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.ValueObjects.TeacherId;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;

public partial class Meeting
{
    [BsonElement("meeting_participants")]
    public ICollection<MeetingSession> MeetingParticipants { get; } = new List<MeetingSession>();

    [NotMapped] public TeacherId TeacherId { get; set; }

    public void AddTeacherToMeeting(string teacherId)
    {
        MeetingParticipants.Add(new MeetingSession(teacherId, Id));
    }

    public void TeacherIdBuilder(string teacherId)
    {
        TeacherId = new TeacherId(teacherId);
    }
}