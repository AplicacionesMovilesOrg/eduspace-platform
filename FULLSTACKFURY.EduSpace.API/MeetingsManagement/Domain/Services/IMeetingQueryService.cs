using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Queries;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Services;

public interface IMeetingQueryService
{
    Task<IEnumerable<Meeting>> Handle(GetAllMeetingsQuery query);
    Task<Meeting?> Handle(GetMeetingByIdQuery query);
    Task<IEnumerable<Meeting>> Handle(GetAllMeetingByAdminIdQuery query);
}