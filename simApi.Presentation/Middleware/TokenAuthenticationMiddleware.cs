using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace simApi.Presentation.Middleware;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _contextAccessor;
    public TokenAuthenticationMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor)
    {
        _next = next;
        _contextAccessor = contextAccessor;
    }
    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
        if (token != null)
        {
            var userId = GetUserIdFromToken(token); 
            var role= GetRolesFromToken(token);
            if (userId != null)
            {
                context.Items["UserId"] = userId; 
            }
            if (role != null)
            {
                context.Items["Roles"] = role;
            }
        }

        await _next(context);
    }
    private string GetUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

        return userId;
    }
    private string GetRolesFromToken(string token)
    {
        var handler=new JwtSecurityTokenHandler();
        var jwtToken=handler.ReadJwtToken(token);
        var roles=jwtToken.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Role)?.Value;

        return roles;
    }
}