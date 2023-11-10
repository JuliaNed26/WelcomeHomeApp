namespace WelcomeHome.Services.Exceptions.ExceptionHandlers;

public abstract class DataExceptionHandler
{
	public DataExceptionHandler? Next { protected get; set; }

	public abstract void Handle(Exception exception);
}
