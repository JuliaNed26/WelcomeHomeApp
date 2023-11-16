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

		public VolunteerService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
        }
        public async Task<VolunteerOutDTO> GetAsync(Guid id)
        {
            var foundVolunteer = await _unitOfWork.VolunteerRepository.GetByIdAsync(id);
            return foundVolunteer == null
                ? throw new RecordNotFoundException("No volunteer with such id")
                : _mapper.Map<VolunteerOutDTO>(foundVolunteer);
        }
        public IEnumerable<VolunteerOutDTO> GetAll()
        {
            var volunteers = _unitOfWork.VolunteerRepository.GetAll();
            return volunteers.Select(volunteer => _mapper.Map<VolunteerOutDTO>(volunteer));
        }

        public async Task AddAsync(VolunteerInDTO newVolunteer)
        {
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .VolunteerRepository
			                                                  .AddAsync(_mapper.Map<Volunteer>(newVolunteer)))
		        .ConfigureAwait(false);
        }
        public async Task UpdateAsync(VolunteerOutDTO volunteerWithUpdateInfo)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                    .VolunteerRepository
                    .UpdateAsync(_mapper.Map<Volunteer>(volunteerWithUpdateInfo)))
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .VolunteerRepository
			                                                  .DeleteAsync(id))
		        .ConfigureAwait(false);
        }

        public int GetCount()
        {
            var allVolunteers = _unitOfWork.VolunteerRepository.GetAll();
            return allVolunteers.Count();
        }

        
    }
}
