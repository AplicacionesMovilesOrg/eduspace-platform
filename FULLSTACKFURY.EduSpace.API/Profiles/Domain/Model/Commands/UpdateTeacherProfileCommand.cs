namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;

public record UpdateTeacherProfileCommand(
    string TeacherId,
    string FirstName,
    string LastName,
    string Email,
    string Dni,
    string Address,
    string Phone
);
