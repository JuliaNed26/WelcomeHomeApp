using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO.EventDto;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

namespace WelcomeHome.Services.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
        }

        public async Task AddAsync(EventInDTO newEvent)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .EventRepository
                                                              .AddAsync(_mapper.Map<Event>(newEvent)))
                .ConfigureAwait(false);
        }

        public async Task AddPsychologicalServiceAsync(EventInDTO newEvent)
        {
            var eventType = await _unitOfWork.EventTypeRepository
                                             .GetByNameAsync("психолог")
                                             .ConfigureAwait(false)
                            ?? throw new BusinessException("Event type for psychological services was not found");

            newEvent.EventTypeId = eventType.Id;

            await AddAsync(newEvent).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork.EventRepository.DeleteAsync(id))
                                   .ConfigureAwait(false);
        }

        public IEnumerable<EventOutDTO> GetAll()
        {
            var events = _unitOfWork.EventRepository.GetAll();

            return events.Select(e => _mapper.Map<EventOutDTO>(e));
        }

        public async Task<EventOutDTO> GetAsync(int id)
        {
            var foundEvent = await _unitOfWork.EventRepository.GetByIdAsync(id);
            return foundEvent == null
                ? throw new RecordNotFoundException("No event with such id")
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

            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .EventRepository
                                                              .UpdateAsync(eventEntity))
                .ConfigureAwait(false);
        }
    }
}
