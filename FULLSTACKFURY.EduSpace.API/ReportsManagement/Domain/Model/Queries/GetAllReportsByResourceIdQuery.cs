namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Queries;

public class GetAllReportsByResourceIdQuery
{
    public GetAllReportsByResourceIdQuery(string resourceId)
    {
        if (string.IsNullOrWhiteSpace(resourceId))
            throw new ArgumentException("ResourceId no puede estar vacío.", nameof(resourceId));

        ResourceId = resourceId;
    }

    public string ResourceId { get; }
}