using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Repository;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for Account aggregate
/// </summary>
public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(IMongoCollection<Account> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Find account by username
    /// </summary>
    public async Task<Account?> FindByUsername(string username)
    {
        return await FindAsync(account => account.Username == username);
    }

    /// <summary>
    ///     Check if an account exists with the given username
    /// </summary>
    public bool ExistsByUsername(string username)
    {
        return ExistsAsync(account => account.Username == username).GetAwaiter().GetResult();
    }
}