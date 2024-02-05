using AutoMapper;
using NSubstitute;
using WelcomeHome.DAL.UnitOfWork;

namespace WelcomeHome.Services.Tests.Services;

[TestFixture]
public class BaseServiceFixture
{
    public IUnitOfWork UnitOfWork { get; private set; }
    public IMapper Mapper { get; private set; }

    [OneTimeSetUp]
    public void InitializeMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
        });
        Mapper = mapperConfig.CreateMapper();
    }

    [SetUp]
    public void InitializeUnitOfWork()
    {
        UnitOfWork = Substitute.For<IUnitOfWork>();
    }
}
