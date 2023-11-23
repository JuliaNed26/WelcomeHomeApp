using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.DAL.UnitOfWork;

public interface IUnitOfWork
{
	public IEventRepository EventRepository { get; }

	public ICityRepository CityRepository { get; }

	public ICountryRepository CountryRepository { get; }

	public IVolunteerRepository VolunteerRepository { get; }

	public ISocialPayoutRepository SocialPayoutRepository { get; }

	public IStepRepository StepRepository { get; }

	public IDocumentRepository DocumentRepository { get; }
	public IEstablishmentRepository EstablishmentRepository { get; }
    public IEstablishmentTypeRepository EstablishmentTypeRepository { get; }
	public IUserCategoryRepository UserCategoryRepository { get; }
}
