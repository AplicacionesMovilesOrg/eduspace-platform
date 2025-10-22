using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Repositories;

public interface IMeetingRepository : IBaseRepository<Meeting>
{
    Task<IEnumerable<Meeting>> FindAllByAdminIdAsync(string adminId);
}