using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WelcomeHome.DAL;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;
using WelcomeHome.Services.Services;
using WelcomeHome.Services.Services.EventService;

namespace WelcomeHome.Web;

public static class WebApplicationBuilderExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        builder.Services.AddDbContext<WelcomeHomeDbContext>(options => options
                                                                     .UseSqlServer(builder.Configuration.GetConnectionString("JuliaNConnectionString"))
                                                                     .UseExceptionProcessor());

        builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
        })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<WelcomeHomeDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.VolunteerOnly.ToString(), p =>
                p.RequireRole("volunteer"));
            options.AddPolicy(AuthorizationPolicies.ModeratorOnly.ToString(), p =>
                p.RequireRole("moderator"));
            options.AddPolicy(AuthorizationPolicies.VolunteerOrModerator.ToString(), p =>
                p.RequireRole("volunteer", "moderator"));
        });

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IVolunteerService, VolunteerService>();
        builder.Services.AddScoped<IEventService, EventService>();
        builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
        builder.Services.AddScoped<IDocumentService, DocumentService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ISocialPayoutService, SocialPayoutService>();
        builder.Services.AddScoped<IStepService, StepService>();
        builder.Services.AddScoped<ICityCountryService, CityCountryService>();
        builder.Services.AddScoped<ITokenService, TokenService>();


        builder.Services.AddSingleton<ExceptionHandlerMediatorBase, ExceptionHandlerMediator>();
    }
}
