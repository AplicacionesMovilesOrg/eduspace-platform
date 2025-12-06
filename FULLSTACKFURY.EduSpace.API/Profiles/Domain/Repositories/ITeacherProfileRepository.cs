using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;

public interface ITeacherProfileRepository : IBaseRepository<TeacherProfile>
{
    Task<IEnumerable<TeacherProfile>> FindAllTeachersByAdministratorIdAsync(string id);
    Task<bool> ExistsByTeacherProfileId(string teacherProfileId);
    Task<bool> ExistsByAccountId(string accountId);
    Task RemoveAsync(TeacherProfile teacherProfile);
    Task<TeacherProfile?> FindByAccountIdAsync(AccountId accountId);
}