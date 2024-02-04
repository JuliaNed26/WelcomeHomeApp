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

        private Mock<IAuthService> _mockAuthService;

        private ExceptionHandlerMediator _exceptionHandlerMediator;
        private static readonly int Guid = 1;

        [SetUp]
        public void Setup()
        {
            _mockVolunteerRepository = new Mock<IVolunteerRepository>();
            _mockMapper = new Mock<IMapper>();
            _exceptionHandlerMediator = new ExceptionHandlerMediator(); // Replace with proper initialization

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository).Returns(_mockVolunteerRepository.Object);

            _mockAuthService = new Mock<IAuthService>();

            _volunteerService = new VolunteerService(_mockUnitOfWork.Object, _mockMapper.Object,
                    _exceptionHandlerMediator, _mockAuthService.Object);
        }



        [Test]
        public async Task GetAsync_WhenVolunteerExists_ReturnsVolunteerOutDTO()
        {
            // Arrange
            var volunteerOutDto = new VolunteerOutDTO { Id = Guid, SocialUrl = "@asdaa" };

            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.GetByIdAsync(Guid)).ReturnsAsync(new Volunteer { UserId = Guid });
            _mockMapper.Setup(mapper => mapper.Map<VolunteerOutDTO>(It.IsAny<Volunteer>())).Returns(volunteerOutDto);

            // Act
            var result = await _volunteerService.GetAsync(Guid);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(Guid, Is.EqualTo(result.Id));
            Assert.That(result.SocialUrl, Is.EqualTo(volunteerOutDto.SocialUrl));
        }

        [Test]
        public void GetAsync_WhenVolunteerDoesNotExist_ThrowsRecordNotFoundException()
        {
            // Arrange
            int nonExistentId = 2;

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
                new Volunteer { UserId = 1, SocialUrl = "Volunteer 1", User = new User
                {
                    FullName = "Volunteer1", Email = "volunteer1@gmail.com", PhoneNumber = "+1111"
                } },
                new Volunteer { UserId = 2, SocialUrl = "Volunteer 2", User = new User
                {
                    FullName = "Volunteer2", Email = "volunteer2@gmail.com", PhoneNumber = "+2222"
                } },
                new Volunteer { UserId = 3, SocialUrl = "Volunteer 3", User = new User
                {
                    FullName = "Volunteer3", Email = "volunteer3@gmail.com", PhoneNumber = "+3333"
                } }
            };

            var expectedVolunteerOutDTOs = volunteers.Select(volunteer => new VolunteerOutDTO { Id = volunteer.UserId, SocialUrl = volunteer.SocialUrl }).ToList();

            _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.GetAll()).Returns(volunteers);
            _mockMapper.Setup(mapper => mapper.Map<VolunteerOutDTO>(It.IsAny<Volunteer>()))
                .Returns<Volunteer>(volunteer => new VolunteerOutDTO { Id = volunteer.UserId, SocialUrl = volunteer.SocialUrl });

            // Act
            var result = _volunteerService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(expectedVolunteerOutDTOs.Count));

            for (var i = 0; i < expectedVolunteerOutDTOs.Count; i++)
            {
                Assert.That(result.ElementAt(i).Id, Is.EqualTo(expectedVolunteerOutDTOs[i].Id));
                Assert.That(result.ElementAt(i).SocialUrl, Is.EqualTo(expectedVolunteerOutDTOs[i].SocialUrl));
            }
        }

        [Test]
        public async Task RegisterVolunteerAsync_WhenVolunteerAdded_SuccessfulAddition()
        {
            // Arrange
            var volunteerRegisterDto = new VolunteerRegisterDTO
            {
                FullName = "John Doe",
                PhoneNumber = "123456789",
                Email = "john.doe@example.com",
                SocialUrl = "telegram1",
                Password = "Password123"
            };

            var userRegisteredDto = new UserRegisterDTO()
            {
                FullName = volunteerRegisterDto.FullName,
                PhoneNumber = volunteerRegisterDto.PhoneNumber,
                Email = volunteerRegisterDto.Email,
                Password = volunteerRegisterDto.Password
            };


            var registeredUser = new User
            {
                FullName = userRegisteredDto.FullName,
                PhoneNumber = userRegisteredDto.PhoneNumber,
                Email = userRegisteredDto.Email,
            };

            var addedVolunteer = new Volunteer
            {
                UserId = 1,
                SocialUrl = volunteerRegisterDto.SocialUrl,
                User = new User
                {
                    FullName = volunteerRegisterDto.FullName,
                    Email = volunteerRegisterDto.Email,
                    PhoneNumber = volunteerRegisterDto.PhoneNumber,
                }
            };
            Volunteer? resultVolunteer = null;




            _mockMapper.Setup(mapper => mapper.Map<UserRegisterDTO>(volunteerRegisterDto)).Returns(userRegisteredDto);
            _mockAuthService.Setup(authService => authService.RegisterUserAsync(userRegisteredDto, "volunteer"))
                                                                            .ReturnsAsync(registeredUser);
            _mockMapper.Setup(mapper => mapper.Map<Volunteer>(volunteerRegisterDto)).Returns(addedVolunteer);


            _mockVolunteerRepository.Setup(repo => repo.AddAsync(It.IsAny<int>(), addedVolunteer))
                    .ReturnsAsync(addedVolunteer);

            // Act
            resultVolunteer = await _volunteerService.RegisterVolunteerAsync(volunteerRegisterDto);

            // Assert
            _mockVolunteerRepository.Verify(repo => repo.AddAsync(It.IsAny<int>(), It.IsAny<Volunteer>()), Times.Once);
            Assert.IsNotNull(resultVolunteer);
            Assert.That(resultVolunteer?.SocialUrl, Is.EqualTo(volunteerRegisterDto.SocialUrl));
        }

        //[Test]
        //public void AddAsync_WhenExceptionThrown_ThrowsException()
        //{
        //    // Arrange
        //    var volunteerInDto = new VolunteerInDTO { SocialUrl = "testTelegram" };
        //    var exceptionMessage = "Test exception message";

        //    _mockMapper.Setup(mapper => mapper.Map<Volunteer>(volunteerInDto)).Returns(It.IsAny<Volunteer>());
        //    _mockVolunteerRepository.Setup(repo => repo.AddAsync(It.IsAny<Volunteer>())).ThrowsAsync(new RecordNotFoundException(exceptionMessage));

        //    // Act & Assert
        //    var exception = Assert.ThrowsAsync<RecordNotFoundException>(async () => await _volunteerService.AddAsync(volunteerInDto));
        //    Assert.That(exception?.Message, Is.EqualTo(exceptionMessage));
        //}

        //[Test]
        //public async Task UpdateAsync_WhenVolunteerUpdated_CheckUpdateAndPropertyName()
        //{
        //    // Arrange
        //    var volunteerOutDto = new VolunteerOutDTO { Id = 1, SocialUrl = "volunteer1" };
        //    var mappedVolunteer = new Volunteer { UserId = volunteerOutDto.Id, SocialUrl = "volunteer1" };
        //    var updatedVolunteer = new Volunteer { UserId = volunteerOutDto.Id, SocialUrl = "updatedVolunteer" };

        //    _mockMapper.Setup(mapper => mapper.Map<Volunteer>(volunteerOutDto)).Returns(mappedVolunteer);

        //    Volunteer? capturedVolunteer = null;
        //    _mockVolunteerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Volunteer>()))
        //        .Callback<Volunteer>(volunteer => capturedVolunteer = volunteer)
        //        .Returns(Task.CompletedTask);

        //    // Act
        //    await _volunteerService.UpdateAsync(volunteerOutDto);

        //    // Assert
        //    _mockVolunteerRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Volunteer>()), Times.Once);

        //    Assert.IsNotNull(capturedVolunteer);
        //    Assert.That(capturedVolunteer?.UserId, Is.EqualTo(volunteerOutDto.Id));
        //}

        //[Test]
        //public async Task DeleteAsync_WhenVolunteerDeleted_DeleteMethodCalledOnceWithCorrectId()
        //{
        //    // Arrange
        //    var volunteerId = 1;

        //    _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.DeleteAsync(volunteerId))
        //        .Returns(Task.CompletedTask);

        //    // Act
        //    await _volunteerService.DeleteAsync(volunteerId);

        //    // Assert
        //    _mockVolunteerRepository.Verify(repo => repo.DeleteAsync(volunteerId), Times.Once);
        //}

        //[Test]
        //public void GetCount_ReturnsTotalVolunteerCount()
        //{
        //    // Arrange
        //    var volunteers = new List<Volunteer>
        //    {
        //        new Volunteer { UserId = 1, SocialUrl = "Volunteer 1" },
        //        new Volunteer { UserId = 2, SocialUrl = "Volunteer 2" },
        //        new Volunteer { UserId = 3, SocialUrl = "Volunteer 3" }
        //    };

        //    _mockUnitOfWork.Setup(uow => uow.VolunteerRepository.GetAll()).Returns(volunteers);

        //    // Act
        //    var result = _volunteerService.GetCount();

        //    // Assert
        //    Assert.That(result, Is.EqualTo(volunteers.Count));
        //}

    }
}
