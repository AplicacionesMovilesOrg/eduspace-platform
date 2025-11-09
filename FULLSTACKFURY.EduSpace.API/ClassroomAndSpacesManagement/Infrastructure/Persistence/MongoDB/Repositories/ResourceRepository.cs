using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for Resource aggregate
/// </summary>
public class ResourceRepository : BaseRepository<Resource>, IResourceRepository
{
    private readonly IMongoCollection<Classroom> _classrooms;

    public ResourceRepository(IMongoCollection<Resource> collection) : base(collection)
    {
        _classrooms = collection.Database.GetCollection<Classroom>("classrooms");
    }

    

    /// <summary>
    ///     Remove a resource asynchronously
    /// </summary>
    public async Task RemoveAsync(Resource entity)
    {
        await base.RemoveAsync(entity.Id);
    }

    public override async Task<IEnumerable<Resource>> ListAsync()
    {
        return await BuildAggregationPipeline().ToListAsync();
    }

    public override async Task<Resource?> FindByIdAsync(string id)
    {
        var filter = ObjectId.TryParse(id, out var objectId)
            ? Builders<Resource>.Filter.Eq("_id", objectId)
            : Builders<Resource>.Filter.Eq("_id", id);

        return await Collection.Find(filter).FirstOrDefaultAsync();
    }


    /// <summary>
    ///     Find resources by classroom ID
    ///     Note: ClassroomId is stored as string in MongoDB, matching the ObjectId format
    /// </summary>
    public async Task<IEnumerable<Resource>> FindByClassroomIdAsync(string classroomId)
    {
        if (!ObjectId.TryParse(classroomId, out var objectId))
            return Enumerable.Empty<Resource>();
        // ClassroomId is stored as string in the database, so we compare as string
        var filter = Builders<Resource>.Filter.Eq("ClassroomId", objectId);

        return await Collection.Aggregate()
            .Match(filter)
            .Lookup("classrooms", "ClassroomId", "_id", "Classroom")
            .Unwind("Classroom")
            .As<Resource>()
            .ToListAsync();
    }


    /// <summary>
    ///     Check if a resource exists with the given name
    /// </summary>
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await ExistsAsync(r => r.Name == name);
    }

    private IAggregateFluent<Resource> BuildAggregationPipeline()
    {
        return Collection.Aggregate()
            .Lookup(
                "classrooms",
                "ClassroomId",
                "_id",
                "Classroom"
            )
            .Unwind("Classroom")
            .As<Resource>();
    }
}