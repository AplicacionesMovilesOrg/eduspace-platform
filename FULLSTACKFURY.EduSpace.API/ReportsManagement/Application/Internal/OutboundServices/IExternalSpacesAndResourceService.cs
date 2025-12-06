namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Application.Internal.OutboundServices;

public interface IExternalSpacesAndResourceService
{
    Task<IEnumerable<string>> GetResourceIdsByTeacherIdAsync(string teacherId);
}