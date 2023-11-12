using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.Services
{
    public sealed class StepService : IStepService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;

        public StepService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<StepOutDTO> GetAsync(Guid id)
        {
            var foundStep = await _unitOfWork.StepRepository.GetByIdAsync(id).ConfigureAwait(false);
            return foundStep == null
                ? throw new RecordNotFoundException("Step was not found")
                : _mapper.Map<StepOutDTO>(foundStep);
        }

        public IEnumerable<StepOutDTO> GetAll()
        {
            return _unitOfWork.StepRepository.GetAll()
                                                 .Select(s => _mapper.Map<StepOutDTO>(s));
        }

        public IEnumerable<StepOutDTO> GetBySocialPayout(Guid payoutId)
        {
            var byPayout = _unitOfWork.StepRepository.GetAll()
                                                       .Where(s => s.PaymentSteps
                                                       .Any(ps => ps.SocialPayoutId == payoutId));
            return byPayout.Select(s => _mapper.Map<StepOutDTO>(s));
        }

        public async Task AddAsync(StepInDTO newStep)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .StepRepository
                                                              .AddAsync(_mapper.Map<Step>(newStep)))
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(StepOutDTO updatedStep)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .StepRepository
                                                              .UpdateAsync(_mapper.Map<Step>(updatedStep)))
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .StepRepository
                                                              .DeleteAsync(id))
                .ConfigureAwait(false);
        }
    }
}
