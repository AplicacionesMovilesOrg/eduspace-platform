using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.OutboundServices.ACL;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Commands.Classroom;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.Internal.CommandServices;

/// <summary>
///     Represents a classroom command service for Classroom entities
/// </summary>
/// <param name="classroomRepository">
///     The repository for classroom entities
/// </param>
/// <param name="unitOfWork">
///     The unit of work for the repository
/// </param>
public class ClassroomCommandService(
    IClassroomRepository classroomRepository,
    IExternalProfileService profileService,
    IUnitOfWork unitOfWork) : IClassroomCommandService
{
    /// <inheritdoc />
    public async Task<Classroom?> Handle(CreateClassroomCommand command)
    {
        if (!await profileService.VerifyProfile(command.TeacherId)) throw new Exception("Teacher not found");
        if (await classroomRepository.ExistsByNameAsync(command.Name))
            throw new Exception("Classroom with the same title already exists");
        var classroom = new Classroom(command);
        await classroomRepository.AddAsync(classroom);
        await unitOfWork.CompleteAsync();
        return classroom;
    }

    public async Task Handle(DeleteClassroomCommand command)
    {
        var classroom = await classroomRepository.FindByIdAsync(command.ClassroomId);
        if (classroom == null) throw new ArgumentException("Classroom not found.");

        await classroomRepository.RemoveAsync(classroom);

        await unitOfWork.CompleteAsync();
    }

    public async Task<Classroom?> Handle(UpdateClassroomCommand command)
    {
        var classroom = await classroomRepository.FindByIdAsync(command.ClassroomId);
        if (classroom == null)
            throw new ArgumentException("Classroom not found.");

        classroom.UpdateName(command.Name);
        classroom.UpdateDescription(command.Description);

        if (!await profileService.VerifyProfile(command.TeacherId))
            throw new ArgumentException("Teacher not found.");
        
        classroom.UpdateTeacherId(command.TeacherId); 

        await classroomRepository.UpdateAsync(classroom);
        await unitOfWork.CompleteAsync();

        return classroom;
    }
}