namespace FULLSTACKFURY.EduSpace.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedAccountResource(string Id, string Username, string Role, string Token);