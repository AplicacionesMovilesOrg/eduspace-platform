using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for TeacherProfile aggregate
/// </summary>
public class TeacherProfileRepository
    : BaseRepository<TeacherProfile>, ITeacherProfileRepository
{
    public TeacherProfileRepository(IMongoCollection<TeacherProfile> collection)
        : base(collection)
    {
    }

    public async Task<IEnumerable<TeacherProfile>> FindAllTeachersByAdministratorIdAsync(string id)
    {
        return await FindAllAsync(teacher => teacher.AdministratorId == id);
    }

    public async Task<bool> ExistsByTeacherProfileId(string teacherProfileId)
    {
        return await ExistsAsync(profile => profile.Id == teacherProfileId);
    }

    public async Task RemoveAsync(TeacherProfile teacherProfile)
    {
        await base.RemoveAsync(teacherProfile.Id);
    }
}