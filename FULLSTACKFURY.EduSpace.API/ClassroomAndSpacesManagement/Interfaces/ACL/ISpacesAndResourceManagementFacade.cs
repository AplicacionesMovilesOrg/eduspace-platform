namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL;

public interface ISpacesAndResourceManagementFacade
{
    Task<bool> ValidateClassroomIdExistence(string classroomId);
    Task<bool> ValidateAreaIdExistence(string areaId);
    Task<IEnumerable<string>> GetResourceIdsByTeacherIdAsync(string teacherId);
}