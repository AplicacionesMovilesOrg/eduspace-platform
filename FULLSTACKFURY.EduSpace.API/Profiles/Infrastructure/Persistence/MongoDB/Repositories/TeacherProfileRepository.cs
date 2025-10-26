using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for TeacherProfile aggregate
/// </summary>
public class TeacherProfileRepository : BaseRepository<TeacherProfile>, ITeacherProfileRepository
{
    public TeacherProfileRepository(IMongoCollection<TeacherProfile> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Find all teachers by administrator ID
    /// </summary>
    public async Task<IEnumerable<TeacherProfile>> FindAllTeachersByAdministratorIdAsync(string id)
    {
        return await FindAllAsync(teacher => teacher.AdministratorId == id);
    }

    /// <summary>
    ///     Check if a teacher profile exists with the given ID
    /// </summary>
    public bool ExistsByTeacherProfileId(string teacherProfileId)
    {
        return ExistsAsync(profile => profile.Id == teacherProfileId).GetAwaiter().GetResult();
    }
}