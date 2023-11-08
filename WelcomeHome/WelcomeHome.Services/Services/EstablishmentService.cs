using AutoMapper;
using System;
using System.ComponentModel;
using System.Text;
using System.Xml;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services;

public sealed class EstablishmentService : IEstablishmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EstablishmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //TODO: Add exceptions

    public async Task<EstablishmentOutDTO> GetAsync(Guid id)
    {
        var foundEstablishment = await _unitOfWork.EstablishmentRepository.GetByIdAsync(id).ConfigureAwait(false);
        return _mapper.Map<EstablishmentOutDTO>(foundEstablishment);
    }

    public IEnumerable<EstablishmentOutDTO> GetAll()
    {
        return _unitOfWork.EstablishmentRepository.GetAll()
                                                  .Select(e => _mapper.Map<EstablishmentOutDTO>(e));
    }

    public IEnumerable<EstablishmentOutDTO> GetByEstablishmentType(Guid typeId)
    {
        return _unitOfWork.EstablishmentRepository.GetAll()
                                                  .Where(e => e.EstablishmentTypeId == typeId)
                                                  .Select(e => _mapper.Map<EstablishmentOutDTO>(e));
    }

    public IEnumerable<EstablishmentOutDTO> GetByCity(Guid cityId)
    {
        return _unitOfWork.EstablishmentRepository.GetAll()
                                                  .Where(e => e.CityId == cityId)
                                                  .Select(e => _mapper.Map<EstablishmentOutDTO>(e));
    }

    public IEnumerable<EstablishmentOutDTO> GetByTypeAndCity(Guid typeId, Guid cityId)
    {
        return GetByEstablishmentType(typeId).Where(e => e.City.Id == cityId);
    }

    public async Task AddAsync(EstablishmentInDTO newEstablishment)
    {
        await _unitOfWork.EstablishmentRepository.AddAsync(_mapper.Map<Establishment>(newEstablishment)).ConfigureAwait(false);
    }

    public async Task UpdateAsync(EstablishmentInDTO updatedEstablishment)
    {
        await _unitOfWork.EstablishmentRepository.UpdateAsync(_mapper.Map<Establishment>(updatedEstablishment)).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.EstablishmentRepository.DeleteAsync(id).ConfigureAwait(false);
    }

    public IEnumerable<EstablishmentTypeOutDTO> GetAllEstablishmentTypes()
    {
        return _unitOfWork.EstablishmentTypeRepository.GetAll()
                                                      .Select(e => _mapper.Map<EstablishmentTypeOutDTO>(e));
    }
}
