using System.Text;
using DotNetEnv;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.Internal.CommandServices;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.Internal.QueryServices;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.OutboundServices.ACL;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Infrastructure.Persistence.MongoDB.Repositories;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL;
using FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Interfaces.ACL.Services;
using FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.CommandServices;
using FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.QueryServices;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Repository;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Services;
using FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Persistence.MongoDB.Repositories;
using FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Pipeline.Middleware.Components;
using FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Toknes.JWT.Configuration;
using FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Toknes.JWT.Services;
using FULLSTACKFURY.EduSpace.API.IAM.Interfaces.ACL;
using FULLSTACKFURY.EduSpace.API.IAM.Interfaces.ACL.Services;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.CommandServices;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Application.Internal.QueryServices;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.MeetingsManagement.Infrastructure.Persistence.MongoDB.Repositories;
using FULLSTACKFURY.EduSpace.API.Profiles.Application.Internal.CommandServices;
using FULLSTACKFURY.EduSpace.API.Profiles.Application.Internal.OutboundServices.ACL;
using FULLSTACKFURY.EduSpace.API.Profiles.Application.Internal.QueryServices;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Profiles.Infrastructure.Persistence.MongoDB.Repositories;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.ACL.Services;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Application.Internal.CommandServices;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Application.Internal.QueryServices;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ReportsManagement.Infrastructure.Persistence.MongoDB.Repositories;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.CommandServices;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.QueryServices;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ReservationsManagement.Infrastructure.Persistence.MongoDB.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Serializers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using IExternalProfileService =
    FULLSTACKFURY.EduSpace.API.ReservationsManagement.Application.Internal.OutboundServices.IExternalProfileService;

if (File.Exists("../.env")) Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Register MongoDB custom serializers
BsonSerializer.RegisterSerializer(new DateOnlySerializer());
BsonSerializer.RegisterSerializer(new TimeOnlySerializer());

var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING")
                       ?? builder.Configuration.GetConnectionString("MongoDbConnection");

Console.WriteLine("==========================================");
Console.WriteLine($"🔍 DEBUG - MongoDB Connected: {!string.IsNullOrEmpty(connectionString)}");
Console.WriteLine($"🔍 DEBUG - Environment: {builder.Environment.EnvironmentName}");
Console.WriteLine($"🔍 DEBUG - Port: {port}");
Console.WriteLine("==========================================");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("MongoDB connection string not found.");

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

    options.AddPolicy("DevelopmentPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "FullStackFury.EduSpacePlatform.API",
            Version = "v1",
            Description = "Eduspace Platform API",
            TermsOfService = new Uri("https://acme-learning.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "Eduspace",
                Email = "contact@fullstackfury.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    c.EnableAnnotations();
});

builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = connectionString;
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME")
                           ?? builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value
                           ?? "eduspacedb";
});

builder.Services.AddSingleton<MongoDbContext>();

//Shared BC
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Profiles BC
builder.Services.AddScoped<IAdminProfileRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new AdminProfileRepository(context.AdminProfiles);
});
builder.Services.AddScoped<ITeacherProfileRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new TeacherProfileRepository(context.TeacherProfiles);
});
builder.Services.AddScoped<IAdminProfileCommandService, AdminProfileCommandService>();
builder.Services.AddScoped<IAdminProfileQueryService, AdminProfileQueryService>();
builder.Services.AddScoped<ITeacherProfileCommandService, TeacherProfileCommandService>();
builder.Services.AddScoped<ITeacherQueryService, TeacherProfileQueryService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddScoped<IExternalIamService, ExternalIamService>();
builder.Services.AddScoped<IAccountCommandService, AccountCommandService>();
builder.Services.AddScoped<IAccountRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new AccountRepository(context.Accounts);
});

builder.Services.AddScoped<IAccountQueryService, AccountQueryService>();
builder.Services.AddScoped<IReservationCommandService, ReservationCommandService>();
builder.Services.AddScoped<IReservationRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new ReservationRepository(context.Reservations);
});
builder.Services.AddScoped<IExternalProfileService, ExternalProfileServices>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();
builder.Services.AddScoped<IReservationQueryService, ReservationQueryService>();

builder.Services.AddScoped<IReportRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new ReportRepository(context.Reports);
});
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();

//Reservation Scheduling
builder.Services.AddScoped<IMeetingRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new MeetingRepository(context.Meetings);
});
builder.Services.AddScoped<IMeetingCommandService, MeetingCommandService>();
builder.Services.AddScoped<IMeetingQueryService, MeetingQueryService>();
builder.Services.AddScoped<IExternalClassroomService, ExternalClassroomServices>();
builder.Services.AddScoped<IRExternalProfileService, RExternalProfileServices>();

// Spaces and Resource BC
builder.Services
    .AddScoped<FULLSTACKFURY.EduSpace.API.ClassroomAndSpacesManagement.Application.OutboundServices.ACL.
        IExternalProfileService, ExternalProfileService>();
builder.Services.AddScoped<IClassroomRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new ClassroomRepository(context.Classrooms);
});
builder.Services.AddScoped<IClassroomCommandService, ClassroomCommandService>();
builder.Services.AddScoped<IClassroomQueryService, ClassroomQueryService>();

builder.Services.AddScoped<IResourceRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new ResourceRepository(context.Resources);
});
builder.Services.AddScoped<IResourceCommandService, ResourceCommandService>();
builder.Services.AddScoped<IResourceQueryService, ResourceQueryService>();

builder.Services.AddScoped<ISpacesAndResourceManagementFacade, SpacesAndResourceManagementFacade>();

builder.Services.AddScoped<ISharedAreaRepository>(sp =>
{
    var context = sp.GetRequiredService<MongoDbContext>();
    return new SharedAreaRepository(context.SharedAreas);
});
builder.Services.AddScoped<ISharedAreaCommandService, SharedAreaCommandService>();
builder.Services.AddScoped<ISharedAreaQueryService, SharedAreaQueryService>();

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? builder.Configuration["TokenSettings:Secret"]
                ?? throw new InvalidOperationException("JWT Secret not configured");

Console.WriteLine("🔑 JWT Secret configured: True");

builder.Services.Configure<TokenSettings>(options => { options.Secret = jwtSecret; });

// JWT Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";
    })
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

if (app.Environment.IsDevelopment())
    app.UseCors("DevelopmentPolicy");
else
    app.UseCors("ProductionPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseRequestAuthorization();

app.MapControllers();

app.Map("/error", (HttpContext http) =>
{
    var exFeature = http.Features.Get<IExceptionHandlerFeature>();
    if (exFeature?.Error == null) return Results.Problem("Unknown error");
    var err = exFeature.Error;
    http.Response.StatusCode = 500;
    return Results.Problem(err.Message, title: "Unhandled exception");
});


app.Run();