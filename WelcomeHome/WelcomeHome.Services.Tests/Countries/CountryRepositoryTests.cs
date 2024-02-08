using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.Repositories;

namespace WelcomeHome.Services.Tests.Countries
{
    [TestFixture]
    public class CountryRepositoryTests
    {
        private DbContextOptions<WelcomeHomeDbContext> _options;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task GetAll_ReturnsAllCountriesWithCities()
        {
            // Arrange
            _options = new DbContextOptionsBuilder<WelcomeHomeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_GetAll_Database")
                .Options;

            await using (var context = new WelcomeHomeDbContext(_options))
            {
                // Add test data
                var country1 = new Country { Id = 1, Name = "Country1", Cities  = new List<City>()};
                country1.Cities.Add(new City { Id = 1, Name = "City1" });
                country1.Cities.Add(new City { Id = 2, Name = "City2" });
                context.Countries.Add(country1);

                var country2 = new Country { Id = 2, Name = "Country2", Cities = new List<City>() };
                country2.Cities.Add(new City { Id = 3, Name = "City3" });
                context.Countries.Add(country2);

                await context.SaveChangesAsync();
            }

            await using (var context = new WelcomeHomeDbContext(_options))
            {
                var countryRepository = new CountryRepository(context);

                // Act
                var result = countryRepository.GetAll().ToList(); 

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.Multiple(() =>
                {
                    Assert.That(result.Any(c => c.Name == "Country1"), Is.True);
                    Assert.That(result.Any(c => c.Name == "Country2"), Is.True);
                });
                var countryWithCities = result.FirstOrDefault(c => c.Name == "Country1");
                Assert.That(countryWithCities, Is.Not.Null);
                Assert.That(countryWithCities?.Cities, Has.Count.EqualTo(2));
            }
        }

        [Test]
        public async Task AddAsync_AddsNewCountry()
        {
            _options = new DbContextOptionsBuilder<WelcomeHomeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddAsync_Database")
                .Options;
            // Arrange
            var countryId = 1;
            var newCountry = new Country
            {
                Id = countryId,
                Name = "NewCountry",
                Cities = new List<City>()
            };

            await using var context = new WelcomeHomeDbContext(_options);
            var countryRepository = new CountryRepository(context);

            // Act
            await countryRepository.AddAsync(newCountry);

            // Assert
            var addedCountry = await context.Countries.FindAsync(countryId);
            Assert.Multiple(() =>
            {
                Assert.That(addedCountry, Is.Not.Null);
                Assert.That(addedCountry?.Name, Is.EqualTo("NewCountry"));
                Assert.That(context.Countries.ToList(), Has.Count.EqualTo(1));
            });
        }

        [Test]
        public async Task DeleteAsync_DeletesExistingCountry()
        {
            _options = new DbContextOptionsBuilder<WelcomeHomeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_DeleteAsync_Database")
                .Options;
            // Arrange
            var countryId = 1;
            var newCountry = new Country
            {
                Id = countryId,
                Name = "CountryToDelete",
                Cities = new List<City>()
            };

            await using var context = new WelcomeHomeDbContext(_options);
            var countryRepository = new CountryRepository(context);

            await context.Countries.AddAsync(newCountry);
            await context.SaveChangesAsync();
                
            // Act
            await countryRepository.DeleteAsync(countryId);

            // Assert
            var deletedCountry = await context.Countries.FindAsync(countryId);
            Assert.Multiple(() =>
            {
                Assert.That(deletedCountry, Is.Null);
                Assert.That(context.Countries.ToList(), Has.Count.EqualTo(0));
            });
        }

        [Test]
        public async Task UpdateAsync_UpdatesExistingCountry()
        {
            _options = new DbContextOptionsBuilder<WelcomeHomeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_UpdateAsync_Database")
                .Options;
            // Arrange
            var countryId = 1;
            var newCountry = new Country
            {
                Id = countryId,
                Name = "CountryToUpdate",
                Cities = new List<City>()
            };

            await using var context = new WelcomeHomeDbContext(_options);
            var countryRepository = new CountryRepository(context);

            await context.Countries.AddAsync(newCountry);
            await context.SaveChangesAsync();

            var countryToUpdate = await context.Countries.FindAsync(countryId);
            countryToUpdate.Name = "UpdatedCountryName";

            // Act
            await countryRepository.UpdateAsync(countryToUpdate);

            // Assert
            var updatedCountry = await context.Countries.FindAsync(countryId);
            Assert.Multiple(() =>
            {
                Assert.That(updatedCountry, Is.Not.Null);
                Assert.That(updatedCountry?.Name, Is.EqualTo("UpdatedCountryName"));
                Assert.That(context.Countries.ToList(), Has.Count.EqualTo(1));
            });
        }
    }

}
