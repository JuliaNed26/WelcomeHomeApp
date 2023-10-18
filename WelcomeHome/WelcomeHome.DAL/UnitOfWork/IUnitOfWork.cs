using WelcomeHome.DAL.Repositories;
using WelcomeHomeModels.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public interface IUnitOfWork
{
	public IEventsRepository EventRepository { get; }

	public IUserRepository UserRepository { get; }
}
