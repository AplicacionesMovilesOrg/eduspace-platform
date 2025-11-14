using FULLSTACKFURY.EduSpace.API.Profiles.Application.Internal.OutboundServices.ACL;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Application.Internal.CommandServices;

public class TeacherProfileCommandService(
    ITeacherProfileRepository teacherProfileRepository,
    IUnitOfWork unitOfWork,
    IExternalIamService externalIamService)
    : ITeacherProfileCommandService
{
    public async Task<TeacherProfile?> Handle(CreateTeacherProfileCommand command)
    {
        try
        {
            var accountId = externalIamService.CreateAccount(command.Username, command.Password, "RoleTeacher");
            var teacherProfile = new TeacherProfile(command, accountId.Result);

            await teacherProfileRepository.AddAsync(teacherProfile);
            await unitOfWork.CompleteAsync();

            return teacherProfile;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the profile {e.Message}");
            return null;
        }
    }

    public async Task Handle(DeleteTeacherProfileCommand command)
    {
        var teacherProfile = await teacherProfileRepository.FindByIdAsync(command.TeacherId);
        if (teacherProfile == null) throw new ArgumentException("Teacher not found");

        await teacherProfileRepository.RemoveAsync(teacherProfile);
        await unitOfWork.CompleteAsync();
    }

    /// <summary>
    ///     Actualiza un TeacherProfile existente.
    /// </summary>
    public async Task<TeacherProfile?> Handle(UpdateTeacherProfileCommand command)
    {
        try
        {
            var teacherProfile = await teacherProfileRepository
                .FindByIdAsync(command.TeacherId);

            if (teacherProfile is null)
                throw new ArgumentException("Teacher not found");

            // LLAMAMOS AL MÉTODO DE DOMINIO
            teacherProfile.UpdateInformation(
                command.FirstName,
                command.LastName,
                command.Email,
                command.Dni,
                command.Address,
                command.Phone
            );

            await teacherProfileRepository.UpdateAsync(teacherProfile);
            await unitOfWork.CompleteAsync();

            return teacherProfile;
        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"An error occurred while updating the profile {e.Message}");
            return null;
        }
    }
}