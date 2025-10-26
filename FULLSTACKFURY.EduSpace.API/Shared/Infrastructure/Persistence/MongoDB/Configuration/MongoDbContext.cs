using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Aggregates;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;

/// <summary>
///     MongoDB database context for EduSpace Platform
///     Provides access to all MongoDB collections for each bounded context
/// </summary>
public class MongoDbContext
{
    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        Database = client.GetDatabase(settings.Value.DatabaseName);

        // Configure MongoDB conventions for consistent naming
        ConfigureConventions();
    }

    // IAM Bounded Context
    public IMongoCollection<Account> Accounts => Database.GetCollection<Account>("accounts");

    // Profiles Bounded Context
    public IMongoCollection<AdminProfile> AdminProfiles => Database.GetCollection<AdminProfile>("admin_profiles");

    public IMongoCollection<TeacherProfile> TeacherProfiles =>
        Database.GetCollection<TeacherProfile>("teacher_profiles");

    public IMongoCollection<Profile> Profiles => Database.GetCollection<Profile>("profiles");

    // Reservations Bounded Context
    public IMongoCollection<Reservation> Reservations => Database.GetCollection<Reservation>("reservations");

    // Reservation Scheduling Bounded Context
    public IMongoCollection<Meeting> Meetings => Database.GetCollection<Meeting>("meetings");
    public IMongoCollection<MeetingAudit> MeetingAudits => Database.GetCollection<MeetingAudit>("meeting_audits");

    // Spaces and Resource Management Bounded Context
    public IMongoCollection<Classroom> Classrooms => Database.GetCollection<Classroom>("classrooms");

    public IMongoCollection<ClassroomAudit> ClassroomAudits =>
        Database.GetCollection<ClassroomAudit>("classroom_audits");

    public IMongoCollection<Resource> Resources => Database.GetCollection<Resource>("resources");
    public IMongoCollection<ResourceAudit> ResourceAudits => Database.GetCollection<ResourceAudit>("resource_audits");
    public IMongoCollection<SharedArea> SharedAreas => Database.GetCollection<SharedArea>("shared_areas");

    public IMongoCollection<SharedAreaAudit> SharedAreaAudits =>
        Database.GetCollection<SharedAreaAudit>("shared_area_audits");

    // Breakdown Management Bounded Context
    public IMongoCollection<Report> Reports => Database.GetCollection<Report>("reports");

    /// <summary>
    ///     Get the MongoDB database instance for advanced operations
    /// </summary>
    public IMongoDatabase Database { get; }

    /// <summary>
    ///     Configure MongoDB naming conventions
    ///     Uses snake_case for element names to match original database schema
    /// </summary>
    private void ConfigureConventions()
    {
        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true),
            new IgnoreIfNullConvention(true)
        };

        ConventionRegistry.Register("EduSpaceConventions", pack, t => true);
    }
}