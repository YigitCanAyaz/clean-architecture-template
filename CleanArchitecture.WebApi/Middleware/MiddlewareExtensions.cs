﻿namespace CleanArchitecture.WebApi.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddlewareExtensions(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}