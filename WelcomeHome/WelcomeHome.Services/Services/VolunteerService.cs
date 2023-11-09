using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public VolunteerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(VolunteerInDTO newVolunteer)
        {
            await _unitOfWork.VolunteerRepository.AddAsync(_mapper.Map<Volunteer>(newVolunteer));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.VolunteerRepository.DeleteAsync(id);
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
		        ? throw new Exception("No volunteer with such id")
		        : _mapper.Map<VolunteerOutDTO>(foundVolunteer);
        }

        public int GetCount()
        {
            var allVolunteers = _unitOfWork.VolunteerRepository.GetAll();
            return allVolunteers.Count();
        }

        public async Task UpdateAsync(VolunteerOutDTO volunteerWithUpdateInfo)
        {
            await _unitOfWork.VolunteerRepository.UpdateAsync(_mapper.Map<Volunteer>(volunteerWithUpdateInfo));
        }
    }
}
