using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web;

public static class WebApplicationBuilderExtensions
{
	public static void RegisterServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

		builder.Services.AddDbContext<WelcomeHomeDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("JuliaNConnectionString")));
		builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
		builder.Services.AddScoped<IUserService, UserService>();
		;
	}
}
