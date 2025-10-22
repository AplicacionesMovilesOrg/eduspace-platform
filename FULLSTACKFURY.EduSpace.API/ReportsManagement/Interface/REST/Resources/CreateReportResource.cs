namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Interface.REST.Resources;

public record CreateReportResource(string KindOfReport, string Description, string ResourceId, DateTime CreatedAt);