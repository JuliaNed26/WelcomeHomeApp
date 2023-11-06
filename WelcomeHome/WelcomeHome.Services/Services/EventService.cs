using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly DataExceptionsHandlerMediator _exceptionHandlerMediator;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper
           /*, Func<Type, DataExceptionsHandlerMediator> exceptionHandlerMediatorFactory*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_exceptionHandlerMediator = exceptionHandlerMediatorFactory(GetType());
        }

        public Task AddAsync(EventInDTO newEvent)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.EventRepository.DeleteAsync(id);
        }

        public IEnumerable<EventOutDTO> GetAll()
        {
            var events = _unitOfWork.EventRepository.GetAll();
            return events.Select(_event => _mapper.Map<EventOutDTO>(_event));
        }

        public async Task<EventOutDTO> GetAsync(Guid id)
        {
            var foundEvent = await _unitOfWork.EventRepository.GetByIdAsync(id);
            return _mapper.Map<UserOutDTO>(foundEvent);
        }

        public int GetCount()
        {
            var allEvents = _unitOfWork.EventRepository.GetAll();
            return allEvents.Count();
        }

        public async Task UpdateAsync(EventInDTO eventWithUpdateInfo)
        {
            var eventEntity = _mapper.Map<Volunteer>(eventWithUpdateInfo);

            await _unitOfWork.VolunteerRepository.UpdateAsync(eventEntity);
        }
    }
}
