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

	public DbSet<User> Users { get; set; }

	public DbSet<City> Cities { get; set; }

	public DbSet<Country> Countries { get; set; }

	public DbSet<Contract> Contracts { get; set; }

	public DbSet<Volunteer> Volunteers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			        .HasIndex(u => u.Email)
			        .IsUnique();
	}
}
