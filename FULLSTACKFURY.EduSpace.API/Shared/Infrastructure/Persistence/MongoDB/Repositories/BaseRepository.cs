using System.Linq.Expressions;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     Base repository implementation for MongoDB operations
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> Collection;

    protected BaseRepository(IMongoCollection<TEntity> collection)
    {
        Collection = collection;
    }

    /// <summary>
    ///     Helper to create filter definitions
    /// </summary>
    protected FilterDefinitionBuilder<TEntity> Filters => Builders<TEntity>.Filter;

    /// <summary>
    ///     Helper to create update definitions
    /// </summary>
    protected UpdateDefinitionBuilder<TEntity> Updates => Builders<TEntity>.Update;

    /// <summary>
    ///     Helper to create sort definitions
    /// </summary>
    protected SortDefinitionBuilder<TEntity> Sorts => Builders<TEntity>.Sort;

    /// <summary>
    ///     Add a new entity to the collection
    /// </summary>
    public async Task AddAsync(TEntity entity)
    {
        await Collection.InsertOneAsync(entity);
    }

    /// <summary>
    ///     Find an entity by its ID
    /// </summary>
    public virtual async Task<TEntity?> FindByIdAsync(string id)
    {
        Console.WriteLine($"Searching for entity with ID: {id}");
        try
        {
            var objectId = ObjectId.Parse(id);
            Console.WriteLine($"Successfully parsed ID to ObjectId: {objectId}");
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            var entity = await Collection.Find(filter).FirstOrDefaultAsync();
            if (entity == null)
                Console.WriteLine("Entity not found.");
            else
                Console.WriteLine("Entity found.");
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing ID or querying database: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    ///     Update an entity in the collection
    /// </summary>
    public virtual void Update(TEntity entity)
    {
        // MongoDB requires async operations, so we'll handle this in repositories
        // For now, this is a placeholder that matches the interface
        throw new NotImplementedException("Use UpdateAsync method in derived repositories");
    }

    /// <summary>
    ///     Remove an entity from the collection
    /// </summary>
    public void Remove(TEntity entity)
    {
        // MongoDB requires async operations, so we'll handle this in repositories
        // For now, this is a placeholder that matches the interface
        throw new NotImplementedException("Use RemoveAsync method in derived repositories");
    }

    /// <summary>
    ///     List all entities in the collection
    /// </summary>
    public virtual async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    // Helper methods for derived repositories

    /// <summary>
    ///     Update an entity by ID
    /// </summary>
    protected async Task UpdateAsync(string id, TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        await Collection.ReplaceOneAsync(filter, entity);
    }

    /// <summary>
    ///     Remove an entity by ID
    /// </summary>
    protected async Task RemoveAsync(string id)
    {
        // Try to parse as ObjectId first, if that fails, use the string directly
        FilterDefinition<TEntity> filter;
        if (ObjectId.TryParse(id, out var objectId))
        {
            filter = Builders<TEntity>.Filter.Eq("_id", objectId);
        }
        else
        {
            filter = Builders<TEntity>.Filter.Eq("_id", id);
        }

        await Collection.DeleteOneAsync(filter);
    }

    /// <summary>
    ///     Find a single entity by predicate
    /// </summary>
    protected async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Collection.Find(predicate).FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Find all entities matching the predicate
    /// </summary>
    protected async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Collection.Find(predicate).ToListAsync();
    }

    /// <summary>
    ///     Count documents matching the predicate
    /// </summary>
    protected async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Collection.CountDocumentsAsync(predicate);
    }

    /// <summary>
    ///     Check if any entity exists matching the predicate
    /// </summary>
    protected async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var count = await Collection.CountDocumentsAsync(predicate);
        return count > 0;
    }
}