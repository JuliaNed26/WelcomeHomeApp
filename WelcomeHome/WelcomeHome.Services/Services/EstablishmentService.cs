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

    public async Task<EstablishmentOutDTO> GetAsync(Guid id)
    {
        var foundEstablishment = await _unitOfWork.EstablishmentRepository.GetByIdAsync(id).ConfigureAwait(false) 
            ?? throw new Exception("Establishment was not found");
        return _mapper.Map<EstablishmentOutDTO>(foundEstablishment);
    }

    public async Task<IEnumerable<EstablishmentOutDTO>> GetAllAsync()
    {
        var establishments = await _unitOfWork.EstablishmentRepository.GetAllAsync();

        var result = establishments.Select(e => _mapper.Map<EstablishmentOutDTO>(e));

        return result;
    }

    public async Task<IEnumerable<EstablishmentOutDTO>> GetByEstablishmentTypeAsync(Guid typeId)
    {
        _ = await _unitOfWork.EstablishmentTypeRepository
                             .GetByIdAsync(typeId)
                             .ConfigureAwait(false)
            //тут поміняти на свої ексепшини
            ?? throw new Exception("Establishment type was not found");

        var establishments = await _unitOfWork.EstablishmentRepository.GetAllAsync();
        var establishmentsByType = establishments.Where(e => e.EstablishmentTypeId == typeId).Select(e => _mapper.Map<EstablishmentOutDTO>(e));
        return establishmentsByType;
    }

    public async Task<IEnumerable<EstablishmentOutDTO>> GetByCityAsync(Guid cityId)
    {
        _ = await _unitOfWork.CityRepository
                             .GetByIdAsync(cityId)
                             .ConfigureAwait(false)
            //exceptions
            ?? throw new Exception("City was not found");

        var establishments = await _unitOfWork.EstablishmentRepository.GetAllAsync();
        var establishmentsByCity = establishments.Where(e => e.CityId == cityId).Select(e => _mapper.Map<EstablishmentOutDTO>(e));
        return establishmentsByCity;
    }

    public async Task<IEnumerable<EstablishmentOutDTO>> GetByTypeAndCityAsync(Guid typeId, Guid cityId)
    {
        var byType = await GetByEstablishmentTypeAsync(typeId);

        _ = await _unitOfWork.CityRepository
                             .GetByIdAsync(cityId)
                             .ConfigureAwait(false)
            //exceptions
            ?? throw new Exception("City was not found");

        return byType.Where(e => e.City.Id == cityId);
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

    public async Task<IEnumerable<EstablishmentTypeOutDTO>> GetAllEstablishmentTypesAsync()
    {
        var establishmentTypes = await _unitOfWork.EstablishmentTypeRepository.GetAllAsync().ConfigureAwait(false);

        var result = establishmentTypes.Select(e => _mapper.Map<EstablishmentTypeOutDTO>(e));

        return result;
    }

   
}
