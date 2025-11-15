namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;

public record CreateTeacherProfileCommand(
    string FirstName,
    string LastName,
    string Email,
    string Dni,
    string Address,
    string Phone,
    string Username,
    string Password,
    string AdministratorId);