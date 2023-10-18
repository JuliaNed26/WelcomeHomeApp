using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
	private readonly WelcomeHomeDbContext _context;

	public UnitOfWork(WelcomeHomeDbContext context)
	{
		_context = context;
	}

	public Lazy<IEventsRepository> EventRepository => new Lazy<IEventsRepository>(() => new EventsRepository(_context));

	public Lazy<IUserRepository> UserRepository => new Lazy<IUserRepository>(() => new UserRepository(_context));
}
