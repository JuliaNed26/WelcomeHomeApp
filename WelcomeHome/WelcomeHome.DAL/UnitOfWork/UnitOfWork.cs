using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
	private readonly Lazy<IEventRepository> _eventRepository;
	private readonly Lazy<ICityRepository> _cityRepository;
	private readonly Lazy<ICountryRepository> _countryRepository;
	private readonly Lazy<IVolunteerRepository> _volunteerRepository;
	private readonly Lazy<ISocialPayoutRepository> _socialPayoutRepository;
	private readonly Lazy<IStepRepository> _stepRepository;
	private readonly Lazy<IDocumentRepository> _documentRepository;
    private readonly Lazy<IEstablishmentRepository> _establishmentRepository;
    private readonly Lazy<IEstablishmentTypeRepository> _establishmentTypeRepository;
	private readonly Lazy<IUserCategoryRepository> _userCategoryRepository;
	private readonly Lazy<IRefreshTokenRepository> _refreshTokenRepository;
    private readonly Lazy<IEventTypeRepository> _eventTypeRepository;

    public UnitOfWork(WelcomeHomeDbContext context)
	{
		_eventRepository = new Lazy<IEventRepository>(() => new EventRepository(context));
		_cityRepository = new Lazy<ICityRepository>(() => new CityRepository(context));
		_countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(context));
		_volunteerRepository = new Lazy<IVolunteerRepository>(() => new VolunteerRepository(context));
		_socialPayoutRepository = new Lazy<ISocialPayoutRepository>(() => new SocialPayoutRepository(context));
		_stepRepository = new Lazy<IStepRepository>(() => new StepRepository(context));
		_documentRepository = new Lazy<IDocumentRepository>(() => new DocumentRepository(context));
		_establishmentRepository=new Lazy<IEstablishmentRepository>(()=> new EstablishmentRepository(context));
        _establishmentTypeRepository = new Lazy<IEstablishmentTypeRepository>(() => new EstablishmentTypeRepository(context));
		_userCategoryRepository = new Lazy<IUserCategoryRepository>(() => new UserCategoryRepository(context));
		_refreshTokenRepository = new Lazy<IRefreshTokenRepository>(() => new RefreshTokenRepository(context));
		_eventTypeRepository = new Lazy<IEventTypeRepository>(() => new EventTypeRepository(context));
    }
  
	public IEventRepository EventRepository => _eventRepository.Value;

	public IEventTypeRepository EventTypeRepository => _eventTypeRepository.Value;

    public IUserCategoryRepository UserCategoryRepository => _userCategoryRepository.Value;

	public ICityRepository CityRepository => _cityRepository.Value;

	public ICountryRepository CountryRepository => _countryRepository.Value;

	public IVolunteerRepository VolunteerRepository => _volunteerRepository.Value;

	public ISocialPayoutRepository SocialPayoutRepository => _socialPayoutRepository.Value;

	public IStepRepository StepRepository => _stepRepository.Value;

	public IDocumentRepository DocumentRepository => _documentRepository.Value;

	public IEstablishmentRepository EstablishmentRepository => _establishmentRepository.Value;

    public IEstablishmentTypeRepository EstablishmentTypeRepository => _establishmentTypeRepository.Value;

	public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository.Value;
}
