using WelcomeHome.Services.Exceptions.ExceptionHandlers;

namespace WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

public abstract class ExceptionHandlerMediatorBase
{
	protected DataExceptionHandler HandlersChain { get; init; }

	public async Task HandleAndThrowAsync(Func<Task> action)
	{
		try
		{
			await action().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			HandleException(ex);
			throw;
		}
	}

	public async Task<T> HandleAndThrowAsync<T>(Func<Task<T>> action)
	{
		try
		{
			var result = await action().ConfigureAwait(false);
			return result;
		}
		catch (Exception ex)
		{
			HandleException(ex);
			throw;
		}
	}

	protected void HandleException(Exception ex)
	{
		HandlersChain.Handle(ex);
	}
}
