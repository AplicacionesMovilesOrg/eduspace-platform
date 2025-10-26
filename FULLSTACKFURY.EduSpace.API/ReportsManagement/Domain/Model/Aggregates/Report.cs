using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Aggregates;

public class Report
{
    public Report()
    {
        KindOfReport = string.Empty;
        Description = string.Empty;
        Status = ReportStatus.EnProceso;
    }

    public Report(string kindOfReport, string description, string resourceId, DateTime createdAt,
        ReportStatus? status = null)
    {
        KindOfReport = kindOfReport;
        Description = description;
        ResourceId = new ResourceId(resourceId);
        CreatedAt = createdAt;
        Status = status ?? ReportStatus.EnProceso; // Default status
    }

    public Report(CreateReportCommand command)
    {
        KindOfReport = command.KindOfReport;
        Description = command.Description;
        ResourceId = new ResourceId(command.ResourceId);
        CreatedAt = command.CreatedAt;
        Status = ReportStatus.EnProceso; // Default status
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("kind_of_report")] public string KindOfReport { get; set; }

    [BsonElement("description")] public string Description { get; set; }

    [BsonElement("resource_id")] public ResourceId ResourceId { get; set; }

    [BsonElement("created_at")] public DateTime CreatedAt { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public string StatusValue { get; set; } // Store as string in MongoDB

    [BsonIgnore]
    public ReportStatus Status
    {
        get => ReportStatus.FromString(StatusValue ?? "in progress");
        set => StatusValue = value?.Value ?? "in progress";
    }
}