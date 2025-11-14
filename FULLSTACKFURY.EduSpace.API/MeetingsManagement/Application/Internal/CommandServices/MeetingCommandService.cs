using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Entities;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.CommandServices;

public class MeetingCommandService(
    IMeetingRepository meetingRepository,
    IUnitOfWork unitOfWork,
    IRExternalProfileService externalProfileService,
    IExternalClassroomService externalClassroomService) : IMeetingCommandService
{
    public async Task<Meeting?> Handle(CreateMeetingCommand command)
    {
        if (!await externalProfileService.ValidateAdminIdExistence(command.AdministratorId))
            throw new ArgumentException("Admin ID does not exist.");

        if (!await externalClassroomService.ValidateClassroomId(command.ClassroomId))
            throw new ArgumentException("Classroom does not exist.");

        var meeting = new Meeting(command);

        await meetingRepository.AddAsync(meeting);

        await unitOfWork.CompleteAsync();

        return meeting;
    }

    public async Task Handle(DeleteMeetingCommand command)
    {
        var meeting = await meetingRepository.FindByIdAsync(command.MeetingId);
        if (meeting == null) throw new ArgumentException("Meeting not found.");

        await meetingRepository.RemoveAsync(meeting);

        await unitOfWork.CompleteAsync();
    }

    public async Task<Meeting?> Handle(UpdateMeetingCommand command)
    {
        var meeting = await meetingRepository.FindByIdAsync(command.MeetingId);
        if (meeting == null)
            throw new ArgumentException("Meeting not found.");

        meeting.UpdateTitle(command.Title);
        meeting.UpdateDescription(command.Description);
        meeting.UpdateDate(command.Date);
        meeting.UpdateTime(command.Start, command.End);

        if (!await externalProfileService.ValidateAdminIdExistence(command.AdministratorId))
            throw new ArgumentException("Admin ID does not exist.");

        if (!await externalClassroomService.ValidateClassroomId(command.ClassroomId))
            throw new ArgumentException("Classroom does not exist.");

        meeting.UpdateAdministrator(command.AdministratorId);
        meeting.UpdateClassroom(command.ClassroomId);

        await meetingRepository.UpdateAsync(meeting);
        await unitOfWork.CompleteAsync();

        return meeting;
    }

    public async Task Handle(AddTeacherToMeetingCommand command)
    {
        var meeting = await meetingRepository.FindByIdAsync(command.MeetingId);

        if (meeting == null)
            throw new ArgumentException("Meeting not found.");

        if (!await externalProfileService.ValidateTeacherExistence(command.TeacherId))
            throw new ArgumentException("Teacher does not exist.");

        var participant = new MeetingSession(command.TeacherId, command.MeetingId);
        await meetingRepository.AddTeacherToMeetingAsync(command.MeetingId, participant);
        await unitOfWork.CompleteAsync();
    }
}