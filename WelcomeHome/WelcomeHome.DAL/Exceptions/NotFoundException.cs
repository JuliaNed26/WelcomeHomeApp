namespace WelcomeHome.DAL.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

	public NotFoundException(string message)
		: base(message)
	{
	}
}
