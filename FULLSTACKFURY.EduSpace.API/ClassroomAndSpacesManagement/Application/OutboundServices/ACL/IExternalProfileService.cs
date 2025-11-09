namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.OutboundServices.ACL;

public interface IExternalProfileService
{
    public  Task<bool> VerifyProfile(string profileId);
}