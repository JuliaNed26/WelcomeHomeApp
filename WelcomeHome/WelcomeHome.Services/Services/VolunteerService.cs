using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

namespace WelcomeHome.Services.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;
        private readonly IAuthService _authService;

        public VolunteerService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
            _authService = authService;
        }

        public async Task<Volunteer?> RegisterVolunteerAsync(VolunteerRegisterDTO newVolunteer)
        {
            var registered = await _authService.RegisterUserAsync(_mapper.Map<UserRegisterDTO>(newVolunteer), "volunteer");

            if (registered != null)
            {
                var result = await _unitOfWork.VolunteerRepository.AddAsync(registered.Id, _mapper.Map<Volunteer>(newVolunteer));

                return result;
            }

            return null;
        }

        public IEnumerable<VolunteerOutDTO> GetAll()
        {
            var volunteers = _unitOfWork.VolunteerRepository.GetAll();

            var result = new List<VolunteerOutDTO>();
            foreach(var volunteer in volunteers)
            {
                var volunteerDto = _mapper.Map<VolunteerOutDTO>(volunteer);
                volunteerDto.FullName = volunteer.User.FullName;
                volunteerDto.PhoneNumber = volunteer.User.PhoneNumber;
                volunteerDto.Email = volunteer.User.Email;
                volunteerDto.Id = volunteer.UserId;

                result.Add(volunteerDto);
                
            }
            return result;
        }

        public async Task<VolunteerOutDTO> GetAsync(Guid id)
        {
            var foundVolunteer = await _unitOfWork.VolunteerRepository.GetByIdAsync(id);
            return foundVolunteer == null
                ? throw new RecordNotFoundException("No volunteer with such id")
                : _mapper.Map<VolunteerOutDTO>(foundVolunteer);
        }
    }
}
