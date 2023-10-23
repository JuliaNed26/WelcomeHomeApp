using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL;

public sealed class WelcomeHomeDbContext : DbContext
{
	public WelcomeHomeDbContext(DbContextOptions<WelcomeHomeDbContext> options)
	{
		Database.EnsureCreated();
	}

	public DbSet<Event> Events { get; set; }
  
  public DbSet<Course> Courses { get; set; }
  
	public DbSet<User> Users { get; set; }
  
  public DbSet<EventType> EventTypes { get; set; }
  
  public DbSet<Establishment> Establishments { get; set; }
  
  public DbSet<EstablishmentType> EstablishmentTypes { get; set; }
  
  public DbSet<SocialPayment> SocialPayments { get; set; }
  
  public DbSet<UserCategory> UserCategories { get; set; }

	public DbSet<City> Cities { get; set; }

	public DbSet<Country> Countries { get; set; }

	public DbSet<Contract> Contracts { get; set; }

	public DbSet<Volunteer> Volunteers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			        .HasIndex(u => u.Email)
			        .IsUnique();

        modelBuilder.Entity<Event>().HasIndex(e => e.Id)
                            .IsUnique();

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

        modelBuilder.Entity<SocialPayment>().HasOne(sp => sp.Establishment)
                                            .WithMany(e => e.SocialPayments)
                                            .HasForeignKey(sp => sp.EstablishmentId)
                                            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SocialPayment>().HasOne(sp => sp.UserCategory)
                                            .WithMany(uc => uc.SocialPayments)
                                            .HasForeignKey(sp => sp.UserCategoryId)
                                            .OnDelete(DeleteBehavior.Cascade);
    }
}
