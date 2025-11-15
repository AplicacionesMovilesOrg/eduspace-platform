using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Entities;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Infrastructure.Persistence.MongoDB.Repositories;

/// <summary>
///     MongoDB repository implementation for Meeting aggregate
/// </summary>
public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
{
    public MeetingRepository(IMongoCollection<Meeting> collection) : base(collection)
    {
    }

    /// <summary>
    ///     Remove a meeting asynchronously
    /// </summary>
    public async Task RemoveAsync(Meeting entity)
    {
        await base.RemoveAsync(entity.Id);
    }

    /// <summary>
    ///     Find all meetings by administrator ID
    /// </summary>
    public async Task<IEnumerable<Meeting>> FindAllByAdminIdAsync(string adminId)
    {
        return await FindAllAsync(m => m.AdministratorId.AdministratorIdentifier == adminId);
    }

    /// <summary>
    ///     Add a teacher to a meeting using MongoDB $push operator to avoid overwriting existing participants
    /// </summary>
    public async Task AddTeacherToMeetingAsync(string meetingId, MeetingSession participant)
    {
        var filter = Builders<Meeting>.Filter.Eq("_id", ObjectId.Parse(meetingId));
        var update = Builders<Meeting>.Update.Push("meeting_participants", participant);
        await Collection.UpdateOneAsync(filter, update);
    }
}