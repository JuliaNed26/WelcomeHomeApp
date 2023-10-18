using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public interface IUnitOfWork
{
	public Lazy<IEventsRepository> EventRepository { get; }

	public Lazy<IUserRepository> UserRepository { get; }
}
