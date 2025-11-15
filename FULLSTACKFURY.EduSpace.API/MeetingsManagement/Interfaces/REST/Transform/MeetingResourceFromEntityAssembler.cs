using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Transform;

public class MeetingResourceFromEntityAssembler
{
    public static MeetingResource ToResourceFromEntity(Meeting entity)
    {
        var teachers = entity.MeetingParticipants
            .Select(mp => new TeacherResource(
                mp.TeacherId,
                mp.Teacher?.ProfileName?.FirstName ?? "Unknown",
                mp.Teacher?.ProfileName?.LastName ?? ""
            ))
            .ToList();

        return new MeetingResource(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.Date,
            entity.StartTime,
            entity.EndTime,
            entity.AdministratorId,
            entity.ClassroomId,
            teachers
        );
    }
}