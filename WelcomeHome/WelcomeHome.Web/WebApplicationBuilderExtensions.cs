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
using WelcomeHome.Services.ServiceClients.RobotaUa;
using WelcomeHome.Services.Services;
using WelcomeHome.Services.Services.EstablishmentService;
using WelcomeHome.Services.Services.EventService;
using WelcomeHome.Services.Services.VacancyService;

namespace WelcomeHome.Web;

public static class WebApplicationBuilderExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        builder.Services.AddDbContext<WelcomeHomeDbContext>(options => options
                                                                     .UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"))
                                                                     .UseExceptionProcessor());

        builder.Services.AddIdentity<User, IdentityRole<long>>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
        })
            .AddRoles<IdentityRole<long>>()
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
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!)),
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.VolunteerOnly.ToString(), p =>
                p.RequireRole("volunteer"));
            options.AddPolicy(AuthorizationPolicies.ModeratorOnly.ToString(), p =>
                p.RequireRole("moderator"));
            options.AddPolicy(AuthorizationPolicies.VerifiedVolunteerOrModerator.ToString(), p =>
            {
                p.RequireAssertion(context => context.User.IsInRole("moderator") ||
                           (context.User.IsInRole("volunteer") &&
                           context.User.HasClaim(nameof(Volunteer.IsVerified), "True")));
            });
            options.AddPolicy(AuthorizationPolicies.VerifiedVolunteerOnly.ToString(), p =>
                p.RequireRole("volunteer")
                 .RequireClaim(nameof(Volunteer.IsVerified), "True"));
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
        builder.Services.AddScoped<IVacancyService, VacancyService>();

        builder.Services.AddScoped<IRobotaUaServiceClient, RobotaUaServiceClient>();

        builder.Services.AddSingleton<ExceptionHandlerMediatorBase, ExceptionHandlerMediator>();
    }
}
