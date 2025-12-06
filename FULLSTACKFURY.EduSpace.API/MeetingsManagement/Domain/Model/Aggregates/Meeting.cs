using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Aggregates;

public partial class Meeting
{
    public Meeting()
    {
    }

    public Meeting(string title, string description, DateOnly date, TimeOnly start, TimeOnly end,
        string administratorId, string classroomId)
    {
        Title = title;
        Description = description;
        Date = date;
        StartTime = start;
        EndTime = end;
        AdministratorId = new AdministratorId(administratorId);
        ClassroomId = new ClassroomId(classroomId);
    }

    public Meeting(CreateMeetingCommand command)
    {
        Title = command.Title;
        Description = command.Description;
        Date = command.Date;
        StartTime = command.Start;
        EndTime = command.End;
        AdministratorId = new AdministratorId(command.AdministratorId);
        ClassroomId = new ClassroomId(command.ClassroomId);
    }

    public Meeting(UpdateMeetingCommand command)
    {
        Description = command.Description;
        Date = command.Date;
        StartTime = command.Start;
        EndTime = command.End;
        AdministratorId = new AdministratorId(command.AdministratorId);
        ClassroomId = new ClassroomId(command.ClassroomId);
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("title")] public string Title { get; private set; } = string.Empty;

    [BsonElement("description")] public string Description { get; private set; } = string.Empty;

    [BsonElement("date")] public DateOnly Date { get; private set; }

    [BsonElement("start_time")] public TimeOnly StartTime { get; private set; }

    [BsonElement("end_time")] public TimeOnly EndTime { get; private set; }

    [BsonElement("administrator_id")] public AdministratorId AdministratorId { get; private set; } = null!;

    [BsonElement("classroom_id")] public ClassroomId ClassroomId { get; private set; } = null!;

    public void UpdateTitle(string? title)
    {
        if (!string.IsNullOrEmpty(title))
            Title = title;
    }

    public void UpdateDescription(string? description)
    {
        if (!string.IsNullOrEmpty(description))
            Description = description;
    }

    public void UpdateDate(DateOnly? date)
    {
        if (date.HasValue)
            Date = date.Value;
    }

    public void UpdateTime(TimeOnly? start, TimeOnly? end)
    {
        if (start.HasValue) StartTime = start.Value;
        if (end.HasValue) EndTime = end.Value;
    }

    public void UpdateAdministrator(string? adminId)
    {
        if (!string.IsNullOrWhiteSpace(adminId))
            AdministratorId = new AdministratorId(adminId);
    }

    public void UpdateClassroom(string? classroomId)
    {
        if (!string.IsNullOrWhiteSpace(classroomId))
            ClassroomId = new ClassroomId(classroomId);
    }
}