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

        public async Task AddAsync(VolunteerInDTO newVolunteer)
        {
            /*
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .VolunteerRepository
			                                                  .AddAsync(_mapper.Map<Volunteer>(newVolunteer)))
		        .ConfigureAwait(false);
            */

            //I didn't figure out how to combine mapper and password shifr, so I wrote my code here 
            byte[] hash;
            byte[] salt;
            _authService.CreatePasswordHash(newVolunteer.Password,out hash, out salt);

            Volunteer volunteer = new Volunteer
            {
                Id = new Guid(),
                PasswordHash = hash,
                PasswordSalt = salt,
                FullName = newVolunteer.FullName,
                PhoneNumber = newVolunteer.PhoneNumber,
                Email = newVolunteer.Email,
                Telegram = newVolunteer.Telegram,
                Document = newVolunteer.Document,
                EstablishmentId = newVolunteer.EstablishmentId == Guid.Empty
                                  ? null
                                  : newVolunteer.EstablishmentId
            };

            await _unitOfWork.VolunteerRepository.AddAsync(volunteer);

            //await _exceptionHandler.HandleAndThrowAsync(()=>_unitOfWork.VolunteerRepository.AddAsync(volunteer));
        }

        public async Task DeleteAsync(Guid id)
        {
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .VolunteerRepository
			                                                  .DeleteAsync(id))
		        .ConfigureAwait(false);
        }

        public IEnumerable<VolunteerOutDTO> GetAll()
        {
            var volunteers = _unitOfWork.VolunteerRepository.GetAll();
            return volunteers.Select(volunteer => _mapper.Map<VolunteerOutDTO>(volunteer));
        }

        public async Task<VolunteerOutDTO> GetAsync(Guid id)
        {
	        var foundVolunteer = await _unitOfWork.VolunteerRepository.GetByIdAsync(id);
	        return foundVolunteer == null
		        ? throw new RecordNotFoundException("No volunteer with such id")
		        : _mapper.Map<VolunteerOutDTO>(foundVolunteer);
        }

        public int GetCount()
        {
            var allVolunteers = _unitOfWork.VolunteerRepository.GetAll();
            return allVolunteers.Count();
        }

        public async Task UpdateAsync(VolunteerOutDTO volunteerWithUpdateInfo)
        {
            //should we update the pswrd?
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .VolunteerRepository
			                                                  .UpdateAsync(_mapper.Map<Volunteer>(volunteerWithUpdateInfo)))
		        .ConfigureAwait(false);
        }
    }
}
