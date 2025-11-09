using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for AdminProfile aggregate
/// </summary>
public class AdminProfileRepository : BaseRepository<AdminProfile>, IAdminProfileRepository
{
    public AdminProfileRepository(IMongoCollection<AdminProfile> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Check if an admin profile exists with the given ID
    /// </summary>
    public async Task<bool> ExistsByAdminProfileId(string adminProfileId)
    {
        return await ExistsAsync(profile => profile.Id == adminProfileId);
    }

    /// <summary>
    ///     Find an admin profile by account ID
    /// </summary>
    public async Task<AdminProfile?> FindByAccountId(AccountId accountId)
    {
        return await FindAsync(profile => profile.AccountId == accountId);
    }
}