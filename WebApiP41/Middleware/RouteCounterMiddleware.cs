using WebApiP41.Services;

namespace WebApiP41.Middleware;

public class RouteCounterMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, RouteCounterService routeCounterService)
    {
        var route = context.Request.Path.ToString();
        var method = context.Request.Method;
        await routeCounterService.IncrementRouteCounterAsync(route, method);
        await next(context);
    }
}