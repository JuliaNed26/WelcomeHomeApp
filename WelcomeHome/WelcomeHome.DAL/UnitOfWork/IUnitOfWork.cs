using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public interface IUnitOfWork
{
	public IEventRepository EventRepository { get; }

	public IUserRepository UserRepository { get; }

	public ICityRepository CityRepository { get; }

	public ICountryRepository CountryRepository { get; }

	public IContractRepository ContractRepository { get; }

	public IVolunteerRepository VolunteerRepository { get; }

	public ISocialPayoutRepository SocialPayoutRepository { get; }

	public IStepRepository StepRepository { get; }
}
