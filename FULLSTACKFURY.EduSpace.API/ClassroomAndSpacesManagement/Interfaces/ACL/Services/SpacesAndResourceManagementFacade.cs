using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL.Services;

public class SpacesAndResourceManagementFacade(IClassroomRepository classroomRepository)
    : ISpacesAndResourceManagementFacade
{
    public async Task<bool> ValidateClassroomIdExistence(string classroomId) 
    {
        return await classroomRepository.ExistsByClassroomIdAsync(classroomId); 
    }
}