using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Services;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Application.Internal.QueryServices;

public class ReportQueryService : IReportQueryService
{
    private readonly IReportRepository _reportRepository;

    public ReportQueryService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public Task<IEnumerable<Report>> Handle(GetAllReportsQuery query)
    {
        return _reportRepository.ListAsync();
    }

    public Task<IEnumerable<Report>> Handle(GetAllReportsByResourceIdQuery query)
    {
        return _reportRepository.FindAllByResourceIdAsync(query.ResourceId);
    }
}