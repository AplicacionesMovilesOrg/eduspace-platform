using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Entities;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Repositories;

public interface IMeetingRepository : IBaseRepository<Meeting>
{
    Task RemoveAsync(Meeting entity);
    Task<IEnumerable<Meeting>> FindAllByAdminIdAsync(string adminId);
    Task AddTeacherToMeetingAsync(string meetingId, MeetingSession participant);
}