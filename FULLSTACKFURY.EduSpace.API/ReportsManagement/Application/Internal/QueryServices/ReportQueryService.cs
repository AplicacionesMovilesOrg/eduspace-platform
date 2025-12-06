using FULLSTACKFURY.EduSpace.API.ReportsManagement.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Services;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Application.Internal.QueryServices;

public class ReportQueryService : IReportQueryService
{
    private readonly IReportRepository _reportRepository;
    private readonly IExternalSpacesAndResourceService _externalSpacesAndResourceService;

    public ReportQueryService(
        IReportRepository reportRepository,
        IExternalSpacesAndResourceService externalSpacesAndResourceService)
    {
        _reportRepository = reportRepository;
        _externalSpacesAndResourceService = externalSpacesAndResourceService;
    }

    public Task<IEnumerable<Report>> Handle(GetAllReportsQuery query)
    {
        return _reportRepository.ListAsync();
    }

    public Task<IEnumerable<Report>> Handle(GetAllReportsByResourceIdQuery query)
    {
        return _reportRepository.FindAllByResourceIdAsync(query.ResourceId);
    }

    public async Task<IEnumerable<Report>> Handle(GetAllReportsByTeacherIdQuery query)
    {
        var resourceIds = await _externalSpacesAndResourceService.GetResourceIdsByTeacherIdAsync(query.TeacherId);
        return await _reportRepository.FindAllByResourceIdsAsync(resourceIds);
    }
}