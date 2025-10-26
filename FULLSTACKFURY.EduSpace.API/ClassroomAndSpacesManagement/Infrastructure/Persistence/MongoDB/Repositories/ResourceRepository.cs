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

    public override void Update(Resource entity)
    {
        var filter = Builders<Resource>.Filter.Eq("_id", ObjectId.Parse(entity.Id));
        Collection.ReplaceOne(filter, entity);
    }

    public override async Task<IEnumerable<Resource>> ListAsync()
    {
        return await BuildAggregationPipeline().ToListAsync();
    }

    public override async Task<Resource?> FindByIdAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        return await BuildAggregationPipeline()
            .Match(r => r.Id == id)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Find resources by classroom ID
    ///     Note: This implementation assumes a ClassroomId property on Resource
    ///     If the relationship is different, this needs adjustment
    /// </summary>
    public async Task<IEnumerable<Resource>> FindByClassroomIdAsync(string classroomId)
    {
        return await BuildAggregationPipeline()
            .Match(r => r.ClassroomId == classroomId)
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