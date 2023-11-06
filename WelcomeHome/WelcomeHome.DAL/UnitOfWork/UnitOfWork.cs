﻿using System.Xml;
using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
	private readonly Lazy<IEventRepository> _eventRepository;
	private readonly Lazy<IUserRepository> _userRepository;
	private readonly Lazy<ICityRepository> _cityRepository;
	private readonly Lazy<ICountryRepository> _countryRepository;
	private readonly Lazy<IContractRepository> _contractRepository;
	private readonly Lazy<IVolunteerRepository> _volunteerRepository;
	private readonly Lazy<ISocialPayoutRepository> _socialPayoutRepository;
	private readonly Lazy<IStepRepository> _stepRepository;
	private readonly Lazy<IDocumentRepository> _documentRepository;
    private readonly Lazy<IEstablishmentRepository> _establishmentRepository;
    private readonly Lazy<IEstablishmentTypeRepository> _establishmentTypeRepository;

    public UnitOfWork(WelcomeHomeDbContext context)
	{
		_eventRepository = new Lazy<IEventRepository>(() => new EventRepository(context));
		_userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
		_cityRepository = new Lazy<ICityRepository>(() => new CityRepository(context));
		_countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(context));
		_contractRepository = new Lazy<IContractRepository>(() => new ContractRepository(context));
		_volunteerRepository = new Lazy<IVolunteerRepository>(() => new VolunteerRepository(context));
		_socialPayoutRepository = new Lazy<ISocialPayoutRepository>(() => new SocialPayoutRepository(context));
		_stepRepository = new Lazy<IStepRepository>(() => new StepRepository(context));
		_documentRepository = new Lazy<IDocumentRepository>(() => new DocumentRepository(context));
		_establishmentRepository=new Lazy<IEstablishmentRepository>(()=> new EstablishmentRepository(context));
        _establishmentTypeRepository = new Lazy<IEstablishmentTypeRepository>(() => new EstablishmentTypeRepository(context));
    }
  
	public IEventRepository EventRepository => _eventRepository.Value;

	public IUserRepository UserRepository => _userRepository.Value;

	public ICityRepository CityRepository => _cityRepository.Value;

	public ICountryRepository CountryRepository => _countryRepository.Value;

	public IContractRepository ContractRepository => _contractRepository.Value;

	public IVolunteerRepository VolunteerRepository => _volunteerRepository.Value;
	public ISocialPayoutRepository SocialPayoutRepository => _socialPayoutRepository.Value;
	public IStepRepository StepRepository => _stepRepository.Value;
	public IDocumentRepository DocumentRepository => _documentRepository.Value;
	public IEstablishmentRepository EstablishmentRepository => _establishmentRepository.Value;
    public IEstablishmentTypeRepository EstablishmentTypeRepository => _establishmentTypeRepository.Value;
}
