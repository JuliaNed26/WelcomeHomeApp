using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WelcomeHome.DAL.EventTypeNameRetriever;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.DTO.EventDto;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;
using WelcomeHome.Services.Services.EventService;
using ServiceForEvents = WelcomeHome.Services.Services.EventService.EventService;

namespace WelcomeHome.Services.Tests.Services.EventService;

[TestFixture]
internal class EventServiceFixture : BaseServiceFixture
{
    private IEventService _eventService;

    [SetUp]
    public void InitializeService()
    {
        _eventService = new ServiceForEvents(
            UnitOfWork,
            Mapper,
            new ExceptionHandlerMediator());
    }

    [Test]
    public void GetPsychologicalServicesAsync_Should_ThrowRecordNotFoundException_When_EventTypeForPsychologicalServiceWasNotFound()
    {
        // Arrange
        UnitOfWork.EventTypeRepository.GetByNameAsync(EventTypeNames.PsychologicalService)
                                      .Returns(Task.FromResult<EventType?>(null));

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _eventService.GetPsychologicalServicesAsync()
                                                                                        .ConfigureAwait(false));
    }

    [Test]
    public void GetAsync_Should_ThrowRecordNotFoundException_When_EventWithIdWasNotFound()
    {
        // Arrange
        var nonExistId = 0;
        UnitOfWork.EventRepository.GetByIdAsync(nonExistId).Returns(Task.FromResult<Event?>(null));

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _eventService.GetAsync(nonExistId)
                                                                                        .ConfigureAwait(false));
    }

    [Test]
    public void AddAsync_Should_ThrowRecordNotFoundException_When_EventVolunteerOrEventTypeWasNotFound()
    {
        // Arrange
        var eventToAdd = new EventInDTO
        {
            EstablishmentId = 0,
            EventTypeId = 0,
            VolunteerId = 0,
            Name = "event",
            Description = "description",
        };
        UnitOfWork.EventRepository.AddAsync(Arg.Any<Event>()).ThrowsAsync<NotFoundException>();

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _eventService.AddAsync(eventToAdd)
                                                                                        .ConfigureAwait(false));
    }

    [Test]
    public void AddPsychologicalServiceAsync_Should_ThrowRecordNotFoundException_When_EventTypeForPsychologicalServiceWasNotFound()
    {
        // Arrange
        var eventToAdd = new EventInDTO
        {
            EstablishmentId = 0,
            VolunteerId = 0,
            Name = "event",
            Description = "description",
        };
        UnitOfWork.EventTypeRepository.GetByNameAsync(EventTypeNames.PsychologicalService)
                                      .Returns(Task.FromResult<EventType?>(null));

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _eventService.AddPsychologicalServiceAsync(eventToAdd)
                                                                                        .ConfigureAwait(false));
    }

    [Test]
    public void UpdateAsync_Should_ThrowRecordNotFoundException_When_EventWithIdWasNotFound()
    {
        // Arrange
        var nonExistEvent = new EventFullInfoDTO
        {
            Id = 0,
            EventTypeId = 1,
            Name = "event",
            Description = "description",
        };
        UnitOfWork.EventRepository.UpdateAsync(Arg.Any<Event>()).ThrowsAsync<DbUpdateConcurrencyException>();

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _eventService.UpdateAsync(nonExistEvent)
                                                                                        .ConfigureAwait(false));
    }

    [Test]
    public void UpdateAsync_Should_ThrowRecordNotFoundException_When_EventVolunteerOrEventTypeWasNotFound()
    {
        // Arrange
        var nonExistEvent = new EventFullInfoDTO
        {
            Id = 1,
            EventTypeId = 0,
            EstablishmentId = 0,
            VolunteerId = 0,
            Name = "event",
            Description = "description",
        };
        UnitOfWork.EventRepository.UpdateAsync(Arg.Any<Event>()).ThrowsAsync<NotFoundException>();

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _eventService.UpdateAsync(nonExistEvent)
                                                                                        .ConfigureAwait(false));
    }

    [Test]
    public void DeleteAsync_Should_ThrowRecordNotFoundException_When_EventWithIdDoesNotExist()
    {
        // Arrange
        var nonExistEventId = 0;
        UnitOfWork.EventRepository.DeleteAsync(nonExistEventId).ThrowsAsync<NotFoundException>();

        // Act & Assert
        Assert.ThrowsAsync<RecordNotFoundException>(async () => await _eventService.DeleteAsync(nonExistEventId)
                                                                                        .ConfigureAwait(false));
    }
}
