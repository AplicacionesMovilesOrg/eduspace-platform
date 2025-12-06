using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for Report aggregate
/// </summary>
public class ReportRepository : BaseRepository<Report>, IReportRepository
{
    public ReportRepository(IMongoCollection<Report> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Find all reports by resource ID
    /// </summary>
    public async Task<IEnumerable<Report>> FindAllByResourceIdAsync(string resourceId)
    {
        return await FindAllAsync(r => r.ResourceId.Id == resourceId);
    }

    /// <summary>
    ///     Find all reports by multiple resource IDs
    /// </summary>
    public async Task<IEnumerable<Report>> FindAllByResourceIdsAsync(IEnumerable<string> resourceIds)
    {
        var resourceIdList = resourceIds.ToList();
        if (!resourceIdList.Any())
            return Enumerable.Empty<Report>();

        return await FindAllAsync(r => resourceIdList.Contains(r.ResourceId.Id));
    }
}