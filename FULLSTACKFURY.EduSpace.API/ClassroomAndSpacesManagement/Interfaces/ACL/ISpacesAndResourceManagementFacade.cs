namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL;

public interface ISpacesAndResourceManagementFacade
{
    Task<bool> ValidateClassroomIdExistence(string classroomId);
    
}