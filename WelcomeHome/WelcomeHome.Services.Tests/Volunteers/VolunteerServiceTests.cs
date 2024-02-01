using AutoMapper;
using Moq;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.Repositories;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Services.Tests.Volunteers
{
    [TestFixture]
    public class VolunteerServiceTests
    {
        private VolunteerService _volunteerService;
        private Mock<IVolunteerRepository> _mockVolunteerRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private ExceptionHandlerMediator _exceptionHandlerMediator;
        private static readonly Guid Guid = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _mockVolunteerRepository = new Mock<IVolunteerRepository>();
            _mockMapper = new Mock<IMapper>();
            _exceptionHandlerMediator = new ExceptionHandlerMediator(); // Replace with proper initialization

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository).Returns(_mockVolunteerRepository.Object);

            _volunteerService = new VolunteerService(_mockUnitOfWork.Object, _mockMapper.Object, _exceptionHandlerMediator);
        }

        [Test]
        public async Task GetAsync_WhenVolunteerExists_ReturnsVolunteerOutDTO()
        {
            // Arrange
            var volunteerOutDto = new VolunteerOutDTO { Id = Guid, Telegram = "@asdaa"};

            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.GetByIdAsync(Guid)).ReturnsAsync(new Volunteer { Id = Guid });
            _mockMapper.Setup(mapper => mapper.Map<VolunteerOutDTO>(It.IsAny<Volunteer>())).Returns(volunteerOutDto);

            // Act
            var result = await _volunteerService.GetAsync(Guid);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(Guid, Is.EqualTo(result.Id));
            Assert.That(result.Telegram, Is.EqualTo(volunteerOutDto.Telegram));
        }

        [Test]
        public void GetAsync_WhenVolunteerDoesNotExist_ThrowsRecordNotFoundException()
        {
            // Arrange
            Guid nonExistentId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.GetByIdAsync(nonExistentId)).ReturnsAsync(null as Volunteer);

            // Act & Assert
            Assert.ThrowsAsync<RecordNotFoundException>(async () => await _volunteerService.GetAsync(nonExistentId));
        }

        [Test]
        public void GetAll_ReturnsAllVolunteersMappedToVolunteerOutDTO()
        {
            // Arrange
            var volunteers = new List<Volunteer>
            {
                new Volunteer { Id = Guid.NewGuid(), Telegram = "Volunteer 1" },
                new Volunteer { Id = Guid.NewGuid(), Telegram = "Volunteer 2" },
                new Volunteer { Id = Guid.NewGuid(), Telegram = "Volunteer 3" }
            };

            var expectedVolunteerOutDTOs = volunteers.Select(volunteer => new VolunteerOutDTO { Id = volunteer.Id, Telegram = volunteer.Telegram }).ToList();

            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.GetAll()).Returns(volunteers);
            _mockMapper.Setup(mapper => mapper.Map<VolunteerOutDTO>(It.IsAny<Volunteer>()))
                .Returns<Volunteer>(volunteer => new VolunteerOutDTO { Id = volunteer.Id, Telegram = volunteer.Telegram });

            // Act
            var result = _volunteerService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(expectedVolunteerOutDTOs.Count));

            for (var i = 0; i < expectedVolunteerOutDTOs.Count; i++)
            {
                Assert.That(result.ElementAt(i).Id, Is.EqualTo(expectedVolunteerOutDTOs[i].Id));
                Assert.That(result.ElementAt(i).Telegram, Is.EqualTo(expectedVolunteerOutDTOs[i].Telegram));
            }
        }

        [Test]
        public async Task AddAsync_WhenVolunteerAdded_SuccessfulAddition()
        {
            // Arrange
            var volunteerInDto = new VolunteerInDTO { Telegram = "telegram1" };
            var mappedVolunteer = new Volunteer { Telegram = "telegram1" };

            Volunteer? capturedVolunteer = null;

            _mockMapper.Setup(mapper => mapper.Map<Volunteer>(volunteerInDto)).Returns(mappedVolunteer);
            _mockVolunteerRepository.Setup(repo => repo.AddAsync(It.IsAny<Volunteer>()))
                                    .Callback<Volunteer>(vol => capturedVolunteer = vol)
                                    .Returns(Task.CompletedTask);

            // Act
            await _volunteerService.AddAsync(volunteerInDto);

            // Assert
            _mockVolunteerRepository.Verify(repo => repo.AddAsync(It.IsAny<Volunteer>()), Times.Once);
            Assert.IsNotNull(capturedVolunteer);
            Assert.That(capturedVolunteer?.Telegram, Is.EqualTo(mappedVolunteer.Telegram));
        }

        [Test]
        public void AddAsync_WhenExceptionThrown_ThrowsException()
        {
            // Arrange
            var volunteerInDto = new VolunteerInDTO { Telegram = "testTelegram"};
            var exceptionMessage = "Test exception message";

            _mockMapper.Setup(mapper => mapper.Map<Volunteer>(volunteerInDto)).Returns(It.IsAny<Volunteer>());
            _mockVolunteerRepository.Setup(repo => repo.AddAsync(It.IsAny<Volunteer>())).ThrowsAsync(new RecordNotFoundException(exceptionMessage));

            // Act & Assert
            var exception = Assert.ThrowsAsync<RecordNotFoundException>(async () => await _volunteerService.AddAsync(volunteerInDto));
            Assert.That(exception?.Message, Is.EqualTo(exceptionMessage));
        }

        [Test]
        public async Task UpdateAsync_WhenVolunteerUpdated_CheckUpdateAndPropertyName()
        {
            // Arrange
            var volunteerOutDto = new VolunteerOutDTO { Id = Guid.NewGuid(), Telegram = "volunteer1" };
            var mappedVolunteer = new Volunteer { Id = volunteerOutDto.Id, Telegram = "volunteer1" };
            var updatedVolunteer = new Volunteer { Id = volunteerOutDto.Id, Telegram = "updatedVolunteer" };

            _mockMapper.Setup(mapper => mapper.Map<Volunteer>(volunteerOutDto)).Returns(mappedVolunteer);

            Volunteer? capturedVolunteer = null;
            _mockVolunteerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Volunteer>()))
                .Callback<Volunteer>(volunteer => capturedVolunteer = volunteer)
                .Returns(Task.CompletedTask);

            // Act
            await _volunteerService.UpdateAsync(volunteerOutDto);

            // Assert
            _mockVolunteerRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Volunteer>()), Times.Once);

            Assert.IsNotNull(capturedVolunteer);
            Assert.That(capturedVolunteer?.Id, Is.EqualTo(volunteerOutDto.Id));
        }

        [Test]
        public async Task DeleteAsync_WhenVolunteerDeleted_DeleteMethodCalledOnceWithCorrectId()
        {
            // Arrange
            var volunteerId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.DeleteAsync(volunteerId))
                .Returns(Task.CompletedTask);

            // Act
            await _volunteerService.DeleteAsync(volunteerId);

            // Assert
            _mockVolunteerRepository.Verify(repo => repo.DeleteAsync(volunteerId), Times.Once);
        }

        [Test]
        public void GetCount_ReturnsTotalVolunteerCount()
        {
            // Arrange
            var volunteers = new List<Volunteer>
            {
                new Volunteer { Id = Guid.NewGuid(), Telegram = "Volunteer 1" },
                new Volunteer { Id = Guid.NewGuid(), Telegram = "Volunteer 2" },
                new Volunteer { Id = Guid.NewGuid(), Telegram = "Volunteer 3" }
            };

            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.GetAll()).Returns(volunteers);

            // Act
            var result = _volunteerService.GetCount();

            // Assert
            Assert.That(result, Is.EqualTo(volunteers.Count));
        }

    }
}
