using EntityFramework.Exceptions.Common;namespace WelcomeHome.Services.Exceptions.ExceptionHandlers;
internal sealed class UniqueConstraintExceptionHandler : DataExceptionHandler
{
	public override void Handle(Exception exception)
	{
		if (exception is UniqueConstraintException)
		{
			throw new BusinessException("Such entity already exists", exception);
		}

		Next?.Handle(exception);
	}
}
