using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.Services.DTO;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.Services
{
    public class VolunteerService: IVolunteerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly DataExceptionsHandlerMediator _exceptionHandlerMediator;

        public VolunteerService(IUnitOfWork unitOfWork, IMapper mapper
            /*, Func<Type, DataExceptionsHandlerMediator> exceptionHandlerMediatorFactory*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_exceptionHandlerMediator = exceptionHandlerMediatorFactory(GetType());
        }

        public async Task AddAsync(VolunteerInDTO newVolunteer)
        {
            //ми це будемо реалізовувати?
            //var validator = ValidatorFactory.GetValidatorForModel(newVolunteer) as AbstractValidator<VolunteerInDTO>;
            //await validator.ValidateAndThrowAsync(newVolunteer).ConfigureAwait(false);

            var volunteerEntity = _mapper.Map<Volunteer>(newVolunteer);
            volunteerEntity.Id = Guid.NewGuid();
            await _unitOfWork.VolunteerRepository.AddAsync(volunteerEntity);
            await AddContract(volunteerEntity.Id);
            //await _exceptionHandlerMediator.HandleAndThrowAsync(() => _unitOfWork.VolunteerRepository.AddAsync(volunteerEntity)).ConfigureAwait(false);

        }

        public async Task DeleteAsync(Guid id)
        {
            //await _exceptionHandlerMediator.HandleAndThrowAsync(() => _unitOfWork.VolunteerRepository.DeleteAsync(id)).ConfigureAwait(false);
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
            return _mapper.Map<VolunteerOutDTO>(foundVolunteer);
        }

        public int GetCount()
        {
            var allVolunteers = _unitOfWork.VolunteerRepository.GetAll();
            return allVolunteers.Count();
        }

        public async Task UpdateAsync(VolunteerInDTO volunteerWithUpdateInfo)
        {
            //var validator = ValidatorFactory.GetValidatorForModel(volunteerWithUpdateInfo) as AbstractValidator<VolunteerInDTO>;
            //await validator.ValidateAndThrowAsync(volunteerWithUpdateInfo).ConfigureAwait(false);

            var volunteerEntity = _mapper.Map<Volunteer>(volunteerWithUpdateInfo);

            //await _exceptionHandlerMediator.HandleAndThrowAsync(() => _unitOfWork.VolunteerRepository.UpdateAsync(volunteerEntity)).ConfigureAwait(false);

            await _unitOfWork.VolunteerRepository.UpdateAsync(volunteerEntity);
        }

        private async void AddContract(Guid volunteerId)
        {
            Contract contract = new Contract
            {
                VolunteerId = volunteerId,
                DateStart = DateTime.Now,
                DateEnd = DateTime.MaxValue,
            };
            await this._unitOfWork.ContractRepository.AddAsync(contract);
        }

    }
}
