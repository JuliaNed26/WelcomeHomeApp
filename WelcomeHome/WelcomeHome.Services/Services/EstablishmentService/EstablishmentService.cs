﻿using AutoMapper;
using WelcomeHome.DAL.Dto;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.DTO.EstablishmentDTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

namespace WelcomeHome.Services.Services.EstablishmentService
{

    public sealed class EstablishmentService : IEstablishmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;

        public EstablishmentService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
        }

        //TODO: AddAsync exceptions

        public async Task<EstablishmentOutDTO> GetAsync(int id)
        {
            var foundEstablishment = await _unitOfWork.EstablishmentRepository.GetByIdAsync(id).ConfigureAwait(false);
            return foundEstablishment == null
                ? throw new RecordNotFoundException("Establishment was not found")
                : _mapper.Map<EstablishmentOutDTO>(foundEstablishment);
        }

        public IEnumerable<EstablishmentOutDTO> GetAll(EstablishmentFiltersDto filters)
        {
            var filtersToRetrieve = _mapper.Map<EstablishmentRetrievalFiltersDto>(filters); 

            return _unitOfWork.EstablishmentRepository.GetAll(filtersToRetrieve)
                                                      .Select(e => _mapper.Map<EstablishmentOutDTO>(e));
        }

        public async Task AddAsync(EstablishmentInDTO newEstablishment)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                             .EstablishmentRepository
                                                             .AddAsync(_mapper.Map<Establishment>(newEstablishment)))
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(EstablishmentOutDTO updatedEstablishment)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .EstablishmentRepository
                                                              .UpdateAsync(_mapper.Map<Establishment>(updatedEstablishment)))
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .EstablishmentRepository
                                                              .DeleteAsync(id))
                .ConfigureAwait(false);
        }

        public IEnumerable<EstablishmentTypeOutDTO> GetAllEstablishmentTypes()
        {
            return _unitOfWork.EstablishmentTypeRepository.GetAll()
                                                          .Select(e => _mapper.Map<EstablishmentTypeOutDTO>(e));
        }
    }
}
