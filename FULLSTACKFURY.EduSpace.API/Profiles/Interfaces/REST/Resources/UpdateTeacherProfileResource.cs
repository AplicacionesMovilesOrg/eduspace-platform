namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Resources;

public class UpdateTeacherProfileResource
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName  { get; set; } = string.Empty;
    public string Email     { get; set; } = string.Empty;
    public string Dni       { get; set; } = string.Empty;
    public string Address   { get; set; } = string.Empty;
    public string Phone     { get; set; } = string.Empty;
}