using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public interface IUnitOfWork
{
	public Lazy<IEventRepository> EventRepository { get; }

	public Lazy<IUserRepository> UserRepository { get; }
}
