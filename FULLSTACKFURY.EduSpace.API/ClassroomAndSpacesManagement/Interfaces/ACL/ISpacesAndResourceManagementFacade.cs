namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL;

public interface ISpacesAndResourceManagementFacade
{
    bool ValidateClassroomIdExistence(string classroomId);
}