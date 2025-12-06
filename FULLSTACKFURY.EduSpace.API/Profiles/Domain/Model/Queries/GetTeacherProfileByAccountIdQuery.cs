using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.ValueObjects;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Queries;

/// <summary>
///     Query to get a teacher profile by account ID
/// </summary>
/// <param name="AccountId">
///     The account ID to search for
/// </param>
public record GetTeacherProfileByAccountIdQuery(AccountId AccountId);
