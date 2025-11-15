using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Interface.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Interface.REST.Transform;

public static class CreateReportCommandFromResourceAssembler
{
    public static CreateReportCommand ToCommandFromResource(CreateReportResource resource)
    {
        return new CreateReportCommand(
            resource.KindOfReport,
            resource.Description,
            resource.ResourceId,
            resource.CreatedAt
        );
    }
}