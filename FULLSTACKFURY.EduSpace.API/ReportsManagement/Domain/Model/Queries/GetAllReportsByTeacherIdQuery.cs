namespace FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Model.Queries;

public class GetAllReportsByTeacherIdQuery
{
    public GetAllReportsByTeacherIdQuery(string teacherId)
    {
        if (string.IsNullOrWhiteSpace(teacherId))
            throw new ArgumentException("TeacherId cannot be empty.", nameof(teacherId));

        TeacherId = teacherId;
    }

    public string TeacherId { get; }
}