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
            teacherId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Dni,
            resource.Address,
            resource.Phone
        );
    }
}