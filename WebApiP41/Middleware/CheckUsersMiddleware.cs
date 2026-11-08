namespace WebApiP41.Middleware;
using WebApiP41.Services;

public class CheckUsersMiddleware
{
    private readonly RequestDelegate _next;
    public CheckUsersMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, UserService service)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/openapi", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/api/users/register", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/api/users/login", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }
        
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        var user = await service.GetUserByTokenAsync(token);
        if (user == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        context.Items["User"] = user;
        await _next(context);
    }
}