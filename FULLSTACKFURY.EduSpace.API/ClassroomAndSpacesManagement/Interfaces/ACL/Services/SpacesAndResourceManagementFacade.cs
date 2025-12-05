using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL.Services;

public class SpacesAndResourceManagementFacade(
    IClassroomRepository classroomRepository,
    ISharedAreaRepository sharedAreaRepository)
    : ISpacesAndResourceManagementFacade
{
    public async Task<bool> ValidateClassroomIdExistence(string classroomId)
    {
        return await classroomRepository.ExistsByClassroomIdAsync(classroomId);
    }

    public async Task<bool> ValidateAreaIdExistence(string areaId)
    {
        var isClassroom = await classroomRepository.ExistsByClassroomIdAsync(areaId);
        if (isClassroom) return true;

        var sharedArea = await sharedAreaRepository.FindByIdAsync(areaId);
        return sharedArea != null;
    }
}