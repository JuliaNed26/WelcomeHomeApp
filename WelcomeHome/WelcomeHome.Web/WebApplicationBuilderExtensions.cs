using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web;

public static class WebApplicationBuilderExtensions
{
	public static void RegisterServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

		builder.Services.AddDbContext<WelcomeHomeDbContext>(options =>options
			                                                         .UseSqlServer(builder.Configuration.GetConnectionString("JuliaNConnectionString"))
			                                                         .UseExceptionProcessor());
			

		builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IVolunteerService, VolunteerService>();
		builder.Services.AddScoped<IEventService, EventService>();
		builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
		builder.Services.AddScoped<IDocumentService, DocumentService>();
		builder.Services.AddScoped<IAuthService, AuthService>();
		builder.Services.AddScoped<ISocialPayoutService, SocialPayoutService>();
		builder.Services.AddScoped<IStepService, StepService>();
		builder.Services.AddScoped<ICityCountryService, CityCountryService>();


		builder.Services.AddSingleton<ExceptionHandlerMediatorBase, ExceptionHandlerMediator>();


	}
}
