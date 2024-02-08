using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;
using WelcomeHome.Services.Services.EstablishmentService;

using ServiceForEstablishment = WelcomeHome.Services.Services.EstablishmentService.EstablishmentService;

namespace WelcomeHome.Services.Tests.Services.EstablishmentService;

[TestFixture]
public class EstablishmentServiceFixture : BaseServiceFixture
{
    private IEstablishmentService _establishmentService;

    [SetUp]
    public void InitializeEstablishmentService()
    {
        _establishmentService = new ServiceForEstablishment(
                                     UnitOfWork,
                                     Mapper,
                                     new ExceptionHandlerMediator());
    }

    [Test]
    public void GetAsync_Should_ThrowRecordNotFoundException_When_EstablishmentWithIdWasNotFound()
    {
        // Arrange
        var nonExistId = 10;
        UnitOfWork.EstablishmentRepository.GetByIdAsync(nonExistId).Returns(Task.FromResult<Establishment?>(null));

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _establishmentService.GetAsync(nonExistId)
                                                                                                .ConfigureAwait(false));
    }

    [Test]
    public void AddAsync_Should_ThrowRecordNotFoundException_When_CityOrEstablishmentTypeForEstablishmentWasNotFound()
    {
        // Arrange
        var establishmentWithIncorrectCityTypeId = new EstablishmentInDTO()
        {
            CityId = 0,
            EstablishmentTypeId = 0,
            Address = string.Empty,
            Name = string.Empty
        };
        UnitOfWork.EstablishmentRepository.AddAsync(Arg.Any<Establishment>())
                                          .ThrowsAsync<NotFoundException>();

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _establishmentService
                                                                           .AddAsync(establishmentWithIncorrectCityTypeId)
                                                                           .ConfigureAwait(false));
    }

    [Test]
    public void UpdateAsync_Should_ThrowRecordNotFoundException_When_EstablishmentWithIdDoesNotExists()
    {
        // Arrange
        var nonExistEstablishment = new EstablishmentFullInfoDTO
        {
            Id = 0,
            CityId = 1,
            EstablishmentTypeId = 1,
            Address = string.Empty,
            Name = string.Empty
        };
        UnitOfWork.EstablishmentRepository.UpdateAsync(Arg.Any<Establishment>())
                                          .ThrowsAsync<DbUpdateConcurrencyException>();

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _establishmentService
            .UpdateAsync(nonExistEstablishment)
            .ConfigureAwait(false));
    }

    [Test]
    public void UpdateAsync_Should_ThrowRecordNotFoundException_When_CityOrEstablishmentTypeForEstablishmentWasNotFound()
    {
        // Arrange
        var establishmentWithIncorrectCityTypeId = new EstablishmentFullInfoDTO()
        {
            Id = 1,
            CityId = 0,
            EstablishmentTypeId = 0,
            Address = string.Empty,
            Name = string.Empty
        };
        UnitOfWork.EstablishmentRepository.UpdateAsync(Arg.Any<Establishment>())
                                          .ThrowsAsync<NotFoundException>();

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _establishmentService
                                                                          .UpdateAsync(establishmentWithIncorrectCityTypeId)
                                                                          .ConfigureAwait(false));
    }

    [Test]
    public void DeleteAsync_Should_ThrowRecordNotFoundException_When_EstablishmentWithIdDoesNotExists()
    {
        // Arrange
        var nonExistEstablishmentId = 0;
        UnitOfWork.EstablishmentRepository.DeleteAsync(nonExistEstablishmentId)
                                          .ThrowsAsync<NotFoundException>();

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _establishmentService
                                                                           .DeleteAsync(nonExistEstablishmentId)
                                                                           .ConfigureAwait(false));
    }
}
