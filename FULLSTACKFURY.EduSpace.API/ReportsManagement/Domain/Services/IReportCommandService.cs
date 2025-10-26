using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Commands;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Services;

public interface IReportCommandService
{
    Task<Report?> Handle(CreateReportCommand command);
}