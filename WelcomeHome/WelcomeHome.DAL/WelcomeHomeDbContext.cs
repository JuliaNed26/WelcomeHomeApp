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
    }
}
