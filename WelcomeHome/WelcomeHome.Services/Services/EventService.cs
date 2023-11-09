using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(EventInDTO newEvent)
        {
            await _unitOfWork.EventRepository.AddAsync(_mapper.Map<Event>(newEvent));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.EventRepository.DeleteAsync(id);
        }

        public IEnumerable<EventOutDTO> GetAll()
        {
            var events = _unitOfWork.EventRepository.GetAll();

            return events.Select(e => _mapper.Map<EventOutDTO>(e));
        }

        public async Task<EventOutDTO> GetAsync(Guid id)
        {
	        var foundEvent = await _unitOfWork.EventRepository.GetByIdAsync(id);
	        return foundEvent == null
		        ? throw new Exception("No event with such id")
		        : _mapper.Map<EventOutDTO>(foundEvent);
        }

        public int GetCount()
        {
            var allEvents = _unitOfWork.EventRepository.GetAll();
            return allEvents.Count();
        }

        public async Task UpdateAsync(EventOutDTO eventWithUpdateInfo)
        {
            var eventEntity = _mapper.Map<Event>(eventWithUpdateInfo);

            await _unitOfWork.EventRepository.UpdateAsync(eventEntity);
        }
    }
}
