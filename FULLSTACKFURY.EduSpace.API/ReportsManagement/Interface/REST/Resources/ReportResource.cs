namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Interface.REST.Resources;

public record ReportResource(
    string Id,
    string KindOfReport,
    string Description,
    string ResourceId,
    DateTime CreatedAt,
    string Status);