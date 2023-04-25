using System.Net;
using TestTaskApplication.API.Helpers;
using TestTaskApplication.API.Models;
using TestTaskApplication.Application.IServices;

namespace TestTaskApplication.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IJournalService journalService)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, journalService);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, IJournalService journalService)
    {
        var queryString = context.Request.QueryString.ToString();
        var requestBody = await context.Request.GetRawBodyAsync();
        var headers = context.Request.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
        await journalService.Save(exception, queryString, requestBody, headers);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Type = exception.GetType().Name,
            Id = exception.HResult,
        }.ToString());
    }
}