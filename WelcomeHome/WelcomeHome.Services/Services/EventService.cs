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

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(EventInDTO newEvent)
        {
            Event eventEntity = ConvertInDTOIntoEntity(newEvent);
            await _unitOfWork.EventRepository.AddAsync(newEvent);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.EventRepository.DeleteAsync(id);
        }

        public IEnumerable<EventOutDTO> GetAll()
        {
            var events = _unitOfWork.EventRepository.GetAll();

            return events.Select(e => ConvertEntityIntoOutDTO(e));
        }

        public async Task<EventOutDTO> GetAsync(Guid id)
        {
            var foundEvent = await _unitOfWork.EventRepository.GetByIdAsync(id);
            if (foundEvent == null)
                throw new Exception("No event with such id");

            return ConvertEntityIntoOutDTO(foundEvent);
        }

        public int GetCount()
        {
            var allEvents = _unitOfWork.EventRepository.GetAll();
            return allEvents.Count();
        }

        public async Task UpdateAsync(EventOutDTO eventWithUpdateInfo)
        {
            var eventEntity = ConvertOutDTOIntoEntity(eventWithUpdateInfo);

            await _unitOfWork.EventRepository.UpdateAsync(eventEntity);
        }

        private Event ConvertInDTOIntoEntity(EventInDTO dto)
        {
            Event entity = new Event
            {
                Id = Guid.NewGuid(),
                Date = dto.Date,
                Name = dto.Name,
                Description = dto.Description,
                EstablishmentId = dto.EstablishmentId,
                EventTypeId = dto.EventTypeId,
                VolunteerId = dto.VolunteerId,
            };
            return entity;
        }

        private EventOutDTO ConvertEntityIntoOutDTO(Event entity)
        {
            EventOutDTO dto = new EventOutDTO
            {
                Id = entity.Id,
                Date = entity.Date,
                Name = entity.Name,
                Description = entity.Description,
                EstablishmentId = entity.EstablishmentId,
                EventTypeId = entity.EventTypeId,
                VolunteerId = entity.VolunteerId,
            };
            return dto;
        }

        private Event ConvertOutDTOIntoEntity(EventOutDTO dto)
        {
            Event entity = new Event
            {
                Id = dto.Id,
                Date = dto.Date,
                Name = dto.Name,
                Description = dto.Description,
                EstablishmentId = dto.EstablishmentId,
                EventTypeId = dto.EventTypeId,
                VolunteerId = dto.VolunteerId,
            };
            return entity;
        }
    }
}
