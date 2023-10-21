using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
	private readonly Lazy<IEventsRepository> _eventRepository;
	private readonly Lazy<IUserRepository> _userRepository;
	private readonly Lazy<ICityRepository> _cityRepository;
	private readonly Lazy<ICountryRepository> _countryRepository;

	public UnitOfWork(WelcomeHomeDbContext context)
	{
		_eventRepository = new Lazy<IEventsRepository>(() => new EventsRepository(context));
		_userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
		_cityRepository = new Lazy<ICityRepository>(() => new CityRepository(context));
		_countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(context));
	}

	public IEventsRepository EventRepository => _eventRepository.Value;

	public IUserRepository UserRepository => _userRepository.Value;

	public ICityRepository CityRepository => _cityRepository.Value;

	public ICountryRepository CountryRepository => _countryRepository.Value;
}
