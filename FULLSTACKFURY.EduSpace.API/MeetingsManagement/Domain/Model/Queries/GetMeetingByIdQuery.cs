namespace FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Model.Queries;

/// <summary>
///     Represents a query to get a meeting by id in the EduSpace API.
/// </summary>
/// <param name="MeetingId">
///     The id of the meeting to get.
/// </param>
public record GetMeetingByIdQuery(string MeetingId);