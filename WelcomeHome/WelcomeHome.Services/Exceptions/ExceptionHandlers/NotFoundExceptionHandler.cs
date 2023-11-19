using WelcomeHome.DAL.Exceptions;

namespace WelcomeHome.Services.Exceptions.ExceptionHandlers;
internal class NotFoundExceptionHandler : DataExceptionHandler
{
	public override void Handle(Exception exception)
	{
		if (exception is NotFoundException)
		{
			throw new RecordNotFoundException(exception.Message, exception);
		}

		Next?.Handle(exception);
	}
}
