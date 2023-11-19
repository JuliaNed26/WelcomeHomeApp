using System.ComponentModel.DataAnnotations;
using System.Net;
using WelcomeHome.Services.Exceptions;
using Newtonsoft.Json;


namespace WelcomeHome.Web.Middleware;

public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionHandlingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (ValidationException ex)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			await AddExceptionMessageToResponseAsync(context, ex).ConfigureAwait(false);
		}
		catch (RecordNotFoundException ex)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			await AddExceptionMessageToResponseAsync(context, ex).ConfigureAwait(false);
		}
		catch (BusinessException ex)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			await AddExceptionMessageToResponseAsync(context, ex).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			await AddExceptionMessageToResponseAsync(context, ex).ConfigureAwait(false);
		}
	}
	private static async Task AddExceptionMessageToResponseAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";

		await context.Response.WriteAsync(JsonConvert.SerializeObject(exception.Message));
	}

}
