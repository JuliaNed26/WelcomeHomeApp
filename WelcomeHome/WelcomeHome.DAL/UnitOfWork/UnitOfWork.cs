using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
	private readonly Lazy<IEventsRepository> _eventRepository;
	private readonly Lazy<IUserRepository> _userRepository;

	public UnitOfWork(WelcomeHomeDbContext context)
	{
		_eventRepository = new Lazy<IEventsRepository>(() => new EventsRepository(context));
		_userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
	}

	public IEventsRepository EventRepository => _eventRepository.Value;

	public IUserRepository UserRepository => _userRepository.Value;
}
