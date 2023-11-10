using WelcomeHome.Services.Exceptions.ExceptionHandlers;

namespace WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

public sealed class ExceptionHandlerMediator : ExceptionHandlerMediatorBase
{
	public ExceptionHandlerMediator()
	{
		HandlersChain = new DbConcurrencyExceptionHandler
		{
			Next = new NotFoundExceptionHandler
			{
				Next = new UniqueConstraintExceptionHandler()
				{
					Next = new ReferenceConstraintExceptionHandler
					{
						Next = new RethrowDataExceptionHandler(),
					},
				},
			},
		};
	}
}
