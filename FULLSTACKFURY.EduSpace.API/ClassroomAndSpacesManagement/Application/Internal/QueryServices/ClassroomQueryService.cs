using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Services;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.Internal.QueryServices;

/// <summary>
///     Classroom query service
/// </summary>
/// <param name="classroomRepository">
///     The classroom repository
/// </param>
public class ClassroomQueryService(IClassroomRepository classroomRepository) : IClassroomQueryService
{
    /// <inheritdoc />
    public async Task<Classroom?> Handle(GetClassroomByIdQuery query)
    {
        return await classroomRepository.FindByIdAsync(query.ClassroomId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Classroom>> Handle(GetAllClassroomsQuery query)
    {
        return await classroomRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Classroom>> Handle(GetAllClassroomsByTeacherIdQuery query)
    {
        return await classroomRepository.FindByTeacherIdAsync(query.TeacherId);
    }
}