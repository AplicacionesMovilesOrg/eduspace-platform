using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL;

namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Application.Internal.OutboundServices;

public class ExternalSpacesAndResourceService : IExternalSpacesAndResourceService
{
    private readonly ISpacesAndResourceManagementFacade _spacesAndResourceManagementFacade;

    public ExternalSpacesAndResourceService(ISpacesAndResourceManagementFacade spacesAndResourceManagementFacade)
    {
        _spacesAndResourceManagementFacade = spacesAndResourceManagementFacade;
    }

    public async Task<IEnumerable<string>> GetResourceIdsByTeacherIdAsync(string teacherId)
    {
        return await _spacesAndResourceManagementFacade.GetResourceIdsByTeacherIdAsync(teacherId);
    }
}