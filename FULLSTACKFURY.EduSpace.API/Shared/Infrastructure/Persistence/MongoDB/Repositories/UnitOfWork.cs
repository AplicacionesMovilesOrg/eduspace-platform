using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     Simplified Unit of Work for MongoDB
///     MongoDB persists changes immediately, so this is essentially a no-op
///     but maintained for interface compatibility
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    ///     Complete the unit of work
    ///     In MongoDB, changes are persisted immediately, so this is a no-op
    /// </summary>
    public Task CompleteAsync()
    {
        // MongoDB operations are atomic and persist immediately
        // No explicit SaveChanges needed
        return Task.CompletedTask;
    }
}