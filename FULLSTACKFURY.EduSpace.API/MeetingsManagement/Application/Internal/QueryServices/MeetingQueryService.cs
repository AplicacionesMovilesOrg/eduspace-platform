using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Services;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.QueryServices;

public class MeetingQueryService(IMeetingRepository meetingRepository) : IMeetingQueryService
{
    public async Task<IEnumerable<Meeting>> Handle(GetAllMeetingsQuery query)
    {
        return await meetingRepository.ListAsync();
    }

    public async Task<Meeting?> Handle(GetMeetingByIdQuery query)
    {
        return await meetingRepository.FindByIdAsync(query.MeetingId);
    }

    public Task<IEnumerable<Meeting>> Handle(GetAllMeetingByAdminIdQuery query)
    {
        return meetingRepository.FindAllByAdminIdAsync(query.AdminId);
    }
}