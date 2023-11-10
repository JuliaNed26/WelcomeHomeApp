using System.Runtime.ExceptionServices;

namespace WelcomeHome.Services.Exceptions.ExceptionHandlers;
internal class RethrowDataExceptionHandler : DataExceptionHandler
{
	public override void Handle(Exception exception)
	{
		ExceptionDispatchInfo.Capture(exception).Throw();
	}
}
