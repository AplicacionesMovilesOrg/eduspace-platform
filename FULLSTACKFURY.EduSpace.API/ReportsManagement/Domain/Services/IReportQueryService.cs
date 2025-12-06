using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Queries;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Services;

public interface IReportQueryService
{
    Task<IEnumerable<Report>> Handle(GetAllReportsQuery query);
    Task<IEnumerable<Report>> Handle(GetAllReportsByResourceIdQuery query);
    Task<IEnumerable<Report>> Handle(GetAllReportsByTeacherIdQuery query);
}