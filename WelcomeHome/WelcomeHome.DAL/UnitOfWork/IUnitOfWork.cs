using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public interface IUnitOfWork
{
	public IEventsRepository EventRepository { get; }

	public IUserRepository UserRepository { get; }

	public ICityRepository CityRepository { get; }

	public ICountryRepository CountryRepository { get; }

	public IContractRepository ContractRepository { get; }
}
