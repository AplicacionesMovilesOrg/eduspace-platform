namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<bool> ValidateTeacherProfileIdExistence(string teacherId); 
    Task<bool> ValidateAdminProfileIdExistence(string adminId); 

}