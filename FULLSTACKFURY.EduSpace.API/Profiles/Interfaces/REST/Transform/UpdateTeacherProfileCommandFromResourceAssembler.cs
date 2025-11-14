using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Transform;

public static class UpdateTeacherProfileCommandFromResourceAssembler
{
    public static UpdateTeacherProfileCommand ToCommand(
        string teacherId,
        UpdateTeacherProfileResource resource)
    {
        return new UpdateTeacherProfileCommand(
            TeacherId: teacherId,
            FirstName: resource.FirstName,
            LastName:  resource.LastName,
            Email:     resource.Email,
            Dni:       resource.Dni,
            Address:   resource.Address,
            Phone:     resource.Phone
        );
    }
}