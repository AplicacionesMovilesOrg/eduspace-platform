using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for SharedArea aggregate
/// </summary>
public class SharedAreaRepository : BaseRepository<SharedArea>, ISharedAreaRepository
{
    public SharedAreaRepository(IMongoCollection<SharedArea> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Check if a shared area exists with the given name
    /// </summary>
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await ExistsAsync(sa => sa.Name == name);
    }
}