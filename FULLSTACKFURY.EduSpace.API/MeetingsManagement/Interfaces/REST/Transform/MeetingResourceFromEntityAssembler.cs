using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Interfaces.REST.Transform;

public class MeetingResourceFromEntityAssembler
{
    public static MeetingResource ToResourceFromEntity(Meeting entity)
    {
        Console.WriteLine($"[MeetingResourceFromEntityAssembler] Meeting ID: {entity.Id}");
        Console.WriteLine($"[MeetingResourceFromEntityAssembler] MeetingParticipants count: {entity.MeetingParticipants?.Count ?? 0}");

        var teachers = entity.MeetingParticipants
            .Select(mp =>
            {
                var hasTeacher = mp.Teacher != null;
                var firstName = mp.Teacher?.ProfileName?.FirstName ?? "Unknown";
                var lastName = mp.Teacher?.ProfileName?.LastName ?? "";

                Console.WriteLine($"[MeetingResourceFromEntityAssembler] Mapping participant {mp.TeacherId}: Teacher loaded={hasTeacher}, Name={firstName} {lastName}");

                return new TeacherResource(
                    mp.TeacherId,
                    firstName,
                    lastName
                );
            })
            .ToList();

        Console.WriteLine($"[MeetingResourceFromEntityAssembler] Total teachers mapped: {teachers.Count}");

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