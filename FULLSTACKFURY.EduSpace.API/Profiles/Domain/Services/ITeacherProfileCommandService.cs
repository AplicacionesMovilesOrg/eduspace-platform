using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Services;

public interface ITeacherProfileCommandService
{
    // CREATE
    Task<TeacherProfile?> Handle(CreateTeacherProfileCommand command);
    
    // UPDATE
    Task<TeacherProfile?> Handle(UpdateTeacherProfileCommand command);
    
    // DELETE
    Task Handle(DeleteTeacherProfileCommand command);
}