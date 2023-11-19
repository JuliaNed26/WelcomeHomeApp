using Microsoft.EntityFrameworkCore;

namespace WelcomeHome.Services.Exceptions.ExceptionHandlers;
internal sealed class DbConcurrencyExceptionHandler : DataExceptionHandler
{
	public override void Handle(Exception exception)
	{
		if (exception is DbUpdateConcurrencyException)
		{
			throw new RecordNotFoundException("Entity was not found", exception);
		}

		Next?.Handle(exception);
	}
}
