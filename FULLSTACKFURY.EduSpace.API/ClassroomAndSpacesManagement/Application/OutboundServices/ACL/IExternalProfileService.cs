namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.OutboundServices.ACL;

public interface IExternalProfileService
{
    public bool VerifyProfile(string profileId);
}