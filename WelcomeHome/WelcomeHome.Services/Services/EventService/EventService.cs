using AutoMapper;
using WelcomeHome.DAL.EventTypeNameRetriever;
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

        public IEnumerable<EventFullInfoDTO> GetAll()
        {
            var events = _unitOfWork.EventRepository.GetAll();
            return events.Select(e => _mapper.Map<EventFullInfoDTO>(e));
        }

        public async Task<IEnumerable<EventFullInfoDTO>> GetPsychologicalServicesAsync()
        {
            var psychologicalServiceType = await GetEventTypeForPsychoServiceAsync().ConfigureAwait(false);
            var psychologicalServices = _unitOfWork.EventRepository
                                                                       .GetByEventType(psychologicalServiceType.Id)
                                                                       .Select(e => _mapper.Map<EventFullInfoDTO>(e));
            return psychologicalServices;
        }

        public async Task<EventFullInfoDTO> GetAsync(int id)
        {
            var foundEvent = await _unitOfWork.EventRepository.GetByIdAsync(id);
            return foundEvent == null
                ? throw new RecordNotFoundException("No event with such id")
                : _mapper.Map<EventFullInfoDTO>(foundEvent);
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
            var psychologicalServiceType = await GetEventTypeForPsychoServiceAsync().ConfigureAwait(false);
            newEvent.EventTypeId = psychologicalServiceType.Id;

            await AddAsync(newEvent).ConfigureAwait(false);
        }

        public async Task UpdateAsync(EventFullInfoDTO eventWithUpdateInfo)
        {
            var eventEntity = _mapper.Map<Event>(eventWithUpdateInfo);

            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                    .EventRepository
                    .UpdateAsync(eventEntity))
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork.EventRepository.DeleteAsync(id))
                                   .ConfigureAwait(false);
        }

        private async Task<EventType> GetEventTypeForPsychoServiceAsync()
        {
            var eventType = await _unitOfWork.EventTypeRepository
                                .GetByNameAsync(EventTypeNames.PsychologicalService)
                                .ConfigureAwait(false)
                            ?? throw new RecordNotFoundException("Event type for psychological service was not found");
            return eventType;
        }
    }
}
