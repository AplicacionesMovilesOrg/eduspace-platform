using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;

/// <summary>
///     Resource Repository Interface
/// </summary>
public interface IResourceRepository : IBaseRepository<Resource>
{
    /// <summary>
    ///     Remove a resource asynchronously
    /// </summary>
    Task RemoveAsync(Resource entity);

    /// <summary>
    ///     Adds a new resource to the repository.
    /// </summary>
    /// <param name="classroomId">
    ///     The classroom id.
    /// </param>
    /// <returns>
    ///     A collection of classrooms that belong to the teacher.
    /// </returns>
    Task<IEnumerable<Resource>> FindByClassroomIdAsync(string classroomId);

    /// <summary>
    ///     Verifies if  a resource exists by its name.
    /// </summary>
    /// <param name="name">
    ///     The name of the resource to verify.
    /// </param>
    Task<bool> ExistsByNameAsync(string name);
}