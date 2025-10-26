using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL.Services;

public class SpacesAndResourceManagementFacade(IClassroomRepository classroomRepository)
    : ISpacesAndResourceManagementFacade
{
    public bool ValidateClassroomIdExistence(string classroomId)
    {
        return classroomRepository.ExistsByClassroomId(classroomId);
    }
}