using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;

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

    public string MeetingId { get; set; }

    public string TeacherId { get; set; }

    public Meeting Meeting { get; set; }
    public TeacherProfile Teacher { get; set; }
}