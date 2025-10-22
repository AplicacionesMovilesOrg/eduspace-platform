namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Commands;

public record CreateReportCommand
{
    public CreateReportCommand(
        string kindOfReport,
        string description,
        string resourceId,
        DateTime createdAt)
    {
        KindOfReport = kindOfReport ?? throw new ArgumentException("El tipo de informe no puede ser nulo o vacío.");
        Description = description;
        ResourceId = !string.IsNullOrWhiteSpace(resourceId)
            ? resourceId
            : throw new ArgumentException("ResourceId no puede estar vacío.");
        CreatedAt = createdAt;
    }

    public string KindOfReport { get; }
    public string Description { get; }
    public string ResourceId { get; }
    public DateTime CreatedAt { get; }
}