using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WelcomeHome.Services.Exceptions;

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
		}
		catch (RecordNotFoundException ex)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)HttpStatusCode.NotFound;
		}
		catch (BusinessException ex)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
		}
		catch (Exception ex)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		}
	}
}
