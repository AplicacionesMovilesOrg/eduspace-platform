using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Transform;

public static class CreateTeacherProfileCommandFromResourceAssembler
{
    public static CreateTeacherProfileCommand ToCommandFromResource(CreateTeacherProfileResource resource,
        string administratorId)
    {
        return new CreateTeacherProfileCommand(resource.FirstName, resource.LastName
            , resource.Email, resource.Dni,
            resource.Address, resource.Phone
            , resource.Username, resource.Password, administratorId);
    }
}