using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.Scripts;

namespace WelcomeHome.DAL;

public sealed class WelcomeHomeDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public WelcomeHomeDbContext(DbContextOptions<WelcomeHomeDbContext> options)
        : base(options)
    {
        Database.EnsureDeleted();
        var dbCreated = Database.EnsureCreated();
        if (dbCreated)
        {
            var connectionString = Database.GetConnectionString()!;
            SeedWithPredefinedData(connectionString);
        }
    }

    public DbSet<Event> Events { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<EventType> EventTypes { get; set; }

    public DbSet<Establishment> Establishments { get; set; }

    public DbSet<EstablishmentType> EstablishmentTypes { get; set; }

    public DbSet<UserCategory> UserCategories { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Volunteer> Volunteers { get; set; }

    public DbSet<Vacancy> Vacancies { get; set; }

    public DbSet<Document> Documents { get; set; }

    public DbSet<Step> Steps { get; set; }

    public DbSet<StepDocument> StepsDocuments { get; set; }

    public DbSet<SocialPayout> SocialPayouts { get; set; }

    public DbSet<PaymentStep> PaymentSteps { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Event>().HasOne(e => e.Volunteer)
                                    .WithMany(v => v.Events)
                                    .HasForeignKey(e => e.VolunteerId)
                                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Event>().HasOne(e => e.EventType)
                                    .WithMany(et => et.Events)
                                    .HasForeignKey(e => e.EventTypeId)
                                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Event>().HasOne(e => e.Establishment)
                                    .WithMany(es => es.Events)
                                    .HasForeignKey(e => e.EstablishmentId)
                                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Establishment>().HasOne(e => e.EstablishmentType)
                                    .WithMany(et => et.Establishments)
                                    .HasForeignKey(e => e.EstablishmentTypeId)
                                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Document>().HasIndex(d => d.Name).IsUnique();

        modelBuilder.Entity<Document>().HasMany(d => d.StepDocuments)
                                       .WithOne(sd => sd.Document)
                                       .HasForeignKey(sd => sd.DocumentId);

        modelBuilder.Entity<Step>().HasMany(s => s.StepDocuments)
                                   .WithOne(sd => sd.Step)
                                   .HasForeignKey(sd => sd.StepId);

        modelBuilder.Entity<EstablishmentType>().HasMany(et => et.Steps)
                                                .WithOne(s => s.EstablishmentType)
                                                .HasForeignKey(s => s.EstablishmentTypeId);

        modelBuilder.Entity<SocialPayout>().HasMany(sp => sp.UserCategories)
                                           .WithMany(uc => uc.SocialPayouts);

        modelBuilder.Entity<SocialPayout>().HasMany(sp => sp.PaymentSteps)
                                           .WithOne(ps => ps.SocialPayout)
                                           .HasForeignKey(ps => ps.SocialPayoutId);

        modelBuilder.Entity<Step>().HasMany(s => s.PaymentSteps)
                                   .WithOne(ps => ps.Step)
                                   .HasForeignKey(ps => ps.StepId);

        modelBuilder.Entity<PaymentStep>().HasKey(ps => new { ps.StepId, ps.SocialPayoutId });
        modelBuilder.Entity<StepDocument>().HasKey(sd => new { sd.StepId, sd.DocumentId });

        modelBuilder.Entity<Volunteer>().HasKey(v => v.UserId);

        modelBuilder.Entity<Volunteer>().HasOne(v => v.User)
                                        .WithOne(u => u.Volunteer)
                                        .HasForeignKey<Volunteer>(u => u.UserId);

        modelBuilder.Entity<User>().HasOne(u => u.RefreshToken)
                                   .WithOne(rt => rt.User);

        modelBuilder.Entity<RefreshToken>().HasOne(rt => rt.User)
                                           .WithOne(u => u.RefreshToken)
                                           .HasForeignKey<RefreshToken>(rt => rt.UserId);

        base.OnModelCreating(modelBuilder);
    }

    private static void SeedWithPredefinedData(string connectionString)
    {
        var solutionDirectoryPath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent!.FullName;

        string countriesSeedScriptPath = Path.Combine(solutionDirectoryPath, "WelcomeHome.DAL\\Scripts\\CountriesSeed.sql");
        SqlScriptExecutor.Execute(countriesSeedScriptPath, connectionString);

        string citiesSeedScriptPath = Path.Combine(solutionDirectoryPath, "WelcomeHome.DAL\\Scripts\\CitiesSeed.sql");
        SqlScriptExecutor.Execute(citiesSeedScriptPath, connectionString);

        string rolesSeedScriptPath = Path.Combine(solutionDirectoryPath, "WelcomeHome.DAL\\Scripts\\RolesSeed.sql");
        SqlScriptExecutor.Execute(rolesSeedScriptPath, connectionString);

        string establishmentTypesSeedScriptPath = Path.Combine(solutionDirectoryPath, "WelcomeHome.DAL\\Scripts\\EstablishmentTypes.sql");
        SqlScriptExecutor.Execute(establishmentTypesSeedScriptPath, connectionString);
    }

}
