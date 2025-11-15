namespace FULLSTACKFURY.EduSpace.API.IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    Task<string> CreateAccount(string username, string password, string role);
}