using EntityFramework.Exceptions.Common;

namespace WelcomeHome.Services.Exceptions.ExceptionHandlers;

internal class ReferenceConstraintExceptionHandler : DataExceptionHandler
{
	public override void Handle(Exception exception)
	{
		if (exception is ReferenceConstraintException)
		{
			throw new RecordNotFoundException("Some relative entities can not be found", exception);
		}

		Next?.Handle(exception);
	}
}