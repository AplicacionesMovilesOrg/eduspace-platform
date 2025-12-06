using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Services;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.QueryServices;

public class MeetingQueryService(
    IMeetingRepository meetingRepository,
    IRExternalProfileService externalProfileService) : IMeetingQueryService
{
    public async Task<IEnumerable<Meeting>> Handle(GetAllMeetingsQuery query)
    {
        var meetings = await meetingRepository.ListAsync();
        await LoadTeachersForMeetings(meetings);
        return meetings;
    }

    public async Task<Meeting?> Handle(GetMeetingByIdQuery query)
    {
        var meeting = await meetingRepository.FindByIdAsync(query.MeetingId);
        if (meeting != null) await LoadTeachersForMeeting(meeting);
        return meeting;
    }

    public async Task<IEnumerable<Meeting>> Handle(GetAllMeetingByAdminIdQuery query)
    {
        var meetings = await meetingRepository.FindAllByAdminIdAsync(query.AdminId);
        await LoadTeachersForMeetings(meetings);
        return meetings;
    }

    private async Task LoadTeachersForMeetings(IEnumerable<Meeting> meetings)
    {
        foreach (var meeting in meetings) await LoadTeachersForMeeting(meeting);
    }

    private async Task LoadTeachersForMeeting(Meeting meeting)
    {
        if (meeting.MeetingParticipants == null || !meeting.MeetingParticipants.Any())
            return;

        var teacherIds = meeting.MeetingParticipants.Select(mp => mp.TeacherId).ToList();
        var teachers = await externalProfileService.GetTeacherProfilesByIds(teacherIds);
        var teachersList = teachers.ToList();

        foreach (var participant in meeting.MeetingParticipants)
            participant.Teacher = teachersList.FirstOrDefault(t => t.Id == participant.TeacherId)!;
    }
}