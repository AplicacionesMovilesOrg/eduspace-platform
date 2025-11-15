namespace FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;

/// <summary>
///     MongoDB configuration settings
/// </summary>
public class MongoDbSettings
{
    /// <summary>
    ///     MongoDB connection string
    /// </summary>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    ///     Database name
    /// </summary>
    public string DatabaseName { get; set; } = null!;
}