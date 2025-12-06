using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Model.Aggregates;

public class Reservation
{
    public Reservation()
    {
        ReservationDate = new ReservationDate();
    }

    public Reservation(string title, DateTime start, DateTime end, string areaId, string teacherId)
    {
        Title = title;
        ReservationDate = new ReservationDate(start, end);
        AreaId = new AreaId(areaId);
        TeacherId = new TeacherId(teacherId);
    }

    public Reservation(CreateReservationCommand command)
    {
        Title = command.Title;
        ReservationDate = new ReservationDate(command.Start, command.End);
        AreaId = new AreaId(command.AreaId);
        TeacherId = new TeacherId(command.TeacherId);
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("title")] public string Title { get; private set; } = string.Empty;

    [BsonElement("reservation_date")] public ReservationDate ReservationDate { get; private set; } = null!;

    [BsonElement("area_id")] public AreaId AreaId { get; private set; } = null!;

    [BsonElement("teacher_id")] public TeacherId TeacherId { get; private set; } = null!;

    public void UpdateReservationDate(DateTime start, DateTime end)
    {
        ReservationDate = new ReservationDate(start, end);
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public bool CanReserve(IEnumerable<Reservation> existingReservations)
    {
        return existingReservations.All(r =>
            (ReservationDate.Start < r.ReservationDate.Start || ReservationDate.Start > r.ReservationDate.End) &&
            (ReservationDate.End < r.ReservationDate.Start || ReservationDate.End > r.ReservationDate.End));
    }
}