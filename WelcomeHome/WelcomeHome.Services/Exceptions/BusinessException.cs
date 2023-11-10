namespace WelcomeHome.Services.Exceptions;

public class BusinessException : Exception
{
	public BusinessException()
		: base()
	{
	}

	public BusinessException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
