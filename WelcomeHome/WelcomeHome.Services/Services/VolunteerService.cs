namespace WelcomeHome.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WelcomeHome.DAL.Models;
    using WelcomeHome.DAL.UnitOfWork;
    using WelcomeHome.Services.DTO;

    public class VolunteerService : IVolunteerService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public VolunteerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(VolunteerInDTO newVolunteer)
        {
            var volunteerEntity = this.ConvertInDTOIntoEntity(newVolunteer);
            await this._unitOfWork.VolunteerRepository.AddAsync(volunteerEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.VolunteerRepository.DeleteAsync(id);
        }

        public IEnumerable<VolunteerOutDTO> GetAll()
        {
            var volunteers = _unitOfWork.VolunteerRepository.GetAll();
            return volunteers.Select(volunteer => ConvertEntityIntoOutDTO(volunteer));
        }

        public async Task<VolunteerOutDTO> GetAsync(Guid id)
        {
            var foundVolunteer = await _unitOfWork.VolunteerRepository.GetByIdAsync(id);
            if (foundVolunteer == null)
            {
                throw new Exception("No volunteer with such id");
            }

            return this.ConvertEntityIntoOutDTO(foundVolunteer);
        }

        public int GetCount()
        {
            var allVolunteers = _unitOfWork.VolunteerRepository.GetAll();
            return allVolunteers.Count();
        }

        public async Task UpdateAsync(VolunteerOutDTO volunteerWithUpdateInfo)
        {
            var volunteerEntity = ConvertOutDTOIntoEntity(volunteerWithUpdateInfo);

            await _unitOfWork.VolunteerRepository.UpdateAsync(volunteerEntity);
        }

        private Volunteer ConvertInDTOIntoEntity(VolunteerInDTO dto)
        {
            Volunteer volunteerEntity = new Volunteer
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Telegram = dto.Telegram,
                Document = dto.Document,
                EstablishmentId = dto.EstablishmentId,
            };
            return volunteerEntity;
        }

        private VolunteerOutDTO ConvertEntityIntoOutDTO (Volunteer entity)
        {
            VolunteerOutDTO dto = new VolunteerOutDTO
            {
                Id = entity.Id,
                FullName = entity.FullName,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Telegram = entity.Telegram,
                Document = entity.Document,
                EstablishmentId = entity.EstablishmentId
            };
            return dto;
        }

        private Volunteer ConvertOutDTOIntoEntity(VolunteerOutDTO dto)
        {
            Volunteer entity = new Volunteer
            {
                Id = dto.Id,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Telegram = dto.Telegram,
                Document = dto.Document,
                EstablishmentId = dto.EstablishmentId
            };
            return entity;
        }
    }
}
