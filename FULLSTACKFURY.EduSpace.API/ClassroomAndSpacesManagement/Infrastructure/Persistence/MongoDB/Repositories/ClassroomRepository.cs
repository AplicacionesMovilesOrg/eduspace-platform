using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for Classroom aggregate
/// </summary>
public class ClassroomRepository : BaseRepository<Classroom>, IClassroomRepository
{
    public ClassroomRepository(IMongoCollection<Classroom> collection) : base(collection)
    {
    }

    public override void Update(Classroom entity)
    {
        var filter = Builders<Classroom>.Filter.Eq("_id", ObjectId.Parse(entity.Id));
        Collection.ReplaceOne(filter, entity);
    }

    /// <summary>
    ///     Remove a classroom by its entity
    /// </summary>
    public async Task RemoveAsync(Classroom entity)
    {
        await base.RemoveAsync(entity.Id);
    }

    /// <summary>
    ///     Find classrooms by teacher ID
    /// </summary>
    public async Task<IEnumerable<Classroom>> FindByTeacherIdAsync(string teacherId)
    {
        return await FindAllAsync(c => c.TeacherId.TeacherIdentifier == teacherId);
    }

    /// <summary>
    ///     Check if a classroom exists with the given name
    /// </summary>
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await ExistsAsync(c => c.Name == name);
    }

    /// <summary>
    ///     Check if a classroom exists with the given ID
    /// </summary>
    public bool ExistsByClassroomId(string id)
    {
        return ExistsAsync(c => c.Id == id).GetAwaiter().GetResult();
    }
}