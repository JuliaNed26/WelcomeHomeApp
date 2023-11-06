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

    public EstablishmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> GetCountAsync()
    {
        var allEstablishments = await _unitOfWork.EstablishmentRepository.GetAllAsync();
        return allEstablishments.Count();
    }

    public async Task<EstablishmentOutDTO> GetAsync(Guid id)
    {
        var foundEstablishment = await _unitOfWork.EstablishmentRepository.GetByIdAsync(id).ConfigureAwait(false);
        var result = ConvertOutDTO(foundEstablishment);
        return result;
    }

    public async Task<IEnumerable<EstablishmentOutDTO>> GetAllAsync()
    {
        var establishments = await _unitOfWork.EstablishmentRepository.GetAllAsync();

        var result = new List<EstablishmentOutDTO>();
        //тимчасове рішення
        foreach (var es in establishments)
        {
            result.Add(ConvertOutDTO(es));
        }
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
        var establishmentsByType = establishments.Where(e => e.EstablishmentTypeId == typeId);
        var result = new List<EstablishmentOutDTO>();
        foreach (var es in establishmentsByType)
        {
            result.Add(ConvertOutDTO(es));
        }
        return result;
    }

    public async Task<IEnumerable<EstablishmentOutDTO>> GetByCityAsync(Guid cityId)
    {
        _ = await _unitOfWork.CityRepository
                             .GetByIdAsync(cityId)
                             .ConfigureAwait(false)
            //exceptions
            ?? throw new Exception("City was not found");

        var establishments = await _unitOfWork.EstablishmentRepository.GetAllAsync();
        var establishmentsByCity = establishments.Where(e => e.CityId == cityId);
        var result = new List<EstablishmentOutDTO>();
        foreach (var es in establishmentsByCity)
        {
            result.Add(ConvertOutDTO(es));
        }
        return result;
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
        var establishmentEntity = await ConvertInDTOAsync(newEstablishment);

        await _unitOfWork.EstablishmentRepository.AddAsync(establishmentEntity).ConfigureAwait(false);

    }

    public async Task UpdateAsync(EstablishmentInDTO updatedEstablishment)
    {
        var establishmentEntity = await ConvertInDTOAsync(updatedEstablishment);

        await _unitOfWork.EstablishmentRepository.UpdateAsync(establishmentEntity).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.EstablishmentRepository.DeleteAsync(id)).ConfigureAwait(false);
    }

    public async Task<IEnumerable<EstablishmentType>> GetAllEstablishmentTypesAsync()
    {
        return await _unitOfWork.EstablishmentTypeRepository.GetAllAsync().ConfigureAwait(false);
    }

    private EstablishmentOutDTO ConvertOutDTO(Establishment establishment)
    {
        EstablishmentOutDTO result = new EstablishmentOutDTO()
        {
            Name = establishment.Name,
            Address = establishment.Address,
            PageURL = establishment.PageURL,
            PhoneNumber = establishment.PhoneNumber,
            OtherContacts = establishment.OtherContacts,
            Events = establishment.Events,
            City = establishment.City,
            EstablishmentType = establishment.EstablishmentType
        };
        return result;
    }
    private async Task<Establishment> ConvertInDTOAsync(EstablishmentInDTO establishmentIn)
    {
        var city = await _unitOfWork.CityRepository
                             .GetByIdAsync(establishmentIn.CityId)
                             .ConfigureAwait(false)
                              ?? throw new Exception("City was not found");
        var type = await _unitOfWork.EstablishmentTypeRepository
                           .GetByIdAsync(establishmentIn.EstablishmentTypeId)
                           .ConfigureAwait(false)
                            ?? throw new Exception("Establishment type was not found");


        var result = new Establishment()
        {
            Name = establishmentIn.Name,
            Address = establishmentIn.Address,
            PageURL = establishmentIn.PageURL,
            PhoneNumber = establishmentIn.PhoneNumber,
            OtherContacts = establishmentIn.OtherContacts,
            City = city,
            EstablishmentType = type
        };
        return result;
    }
}
