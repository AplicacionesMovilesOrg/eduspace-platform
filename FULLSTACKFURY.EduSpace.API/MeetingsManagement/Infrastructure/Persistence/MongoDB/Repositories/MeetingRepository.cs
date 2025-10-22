using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for Meeting aggregate
/// </summary>
public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
{
    public MeetingRepository(IMongoCollection<Meeting> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Find all meetings by administrator ID
    /// </summary>
    public async Task<IEnumerable<Meeting>> FindAllByAdminIdAsync(string adminId)
    {
        return await FindAllAsync(m => m.AdministratorId.AdministratorIdentifier == adminId);
    }
}