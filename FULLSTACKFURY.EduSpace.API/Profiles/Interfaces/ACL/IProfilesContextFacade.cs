namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    bool ValidateTeacherProfileIdExistence(string id);
    bool ValidateAdminProfileIdExistence(string id);
}