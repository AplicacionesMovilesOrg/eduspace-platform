using FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using AllowAnonymousAttribute =
    FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Pipeline.Middleware.Attributes.AllowAnonymousAttribute;

namespace FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IAccountQueryService userQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine($"🔍 RequestAuthorizationMiddleware - Path: {context.Request.Path}");

        // Check if the endpoint has AllowAnonymousAttribute (either custom or ASP.NET Core). If it does, skip authorization
        var endpoint = context.GetEndpoint();

        // If endpoint is null (route not found), skip authorization and let ASP.NET Core handle the 404
        if (endpoint == null)
        {
            Console.WriteLine($"⚠️ Endpoint not found, skipping authorization: {context.Request.Path}");
            await next(context);
            return;
        }

        var allowAnonymous = endpoint.Metadata.Any(m =>
            m is AllowAnonymousAttribute ||
            m is IAllowAnonymous);

        if (allowAnonymous)
        {
            Console.WriteLine($"✅ AllowAnonymous detected for: {context.Request.Path}");
            await next(context);
            return;
        }

        Console.WriteLine($"🔒 Authorization required for: {context.Request.Path}");

        // Check if the endpoint has AuthorizeAttribute. If it does, authorize the request
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        // If token is not found, return 401
        if (token is null) throw new UnauthorizedAccessException("Token not found");
        var userId = await tokenService.ValidateToken(token);
        // If token is invalid, return 401
        if (userId is null) throw new UnauthorizedAccessException("Invalid token");
        var getUserByIdQuery = new GetAccountByIdQuery(userId);
        var user = await userQueryService.Handle(getUserByIdQuery);
        // If user is not found, return 401
        if (user is null) throw new UnauthorizedAccessException("Account not found");
        context.Items["Account"] = user;
        await next(context);
    }
}