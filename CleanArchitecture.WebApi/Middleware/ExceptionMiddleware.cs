using FluentValidation;

namespace CleanArchitecture.WebApi.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            // sonraki middleware'a devam et
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // dışarı doğru basarken json formatına çevirmezsek format bozukluğu yaşanır
        context.Response.ContentType = "applicaiton/json";
        context.Response.StatusCode = 500; // internal server hatası verir

        if (ex.GetType() == typeof(ValidationException))
        {
            return context.Response.WriteAsync(new ValidationErrorDetails
            {
                Errors = ((ValidationException)ex).Errors.Select(s =>
                s.PropertyName),
                StatusCode = 403 // forbidden
            }.ToString());
        }

        return context.Response.WriteAsync(new ErrorResult
        {
            Message = ex.Message,
            StatusCode = context.Response.StatusCode
        }.ToString());
    }
}
