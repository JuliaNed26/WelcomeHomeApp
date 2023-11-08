using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IEstablishmentService
    {
        Task<EstablishmentOutDTO> GetAsync(Guid id);

        Task<IEnumerable<EstablishmentOutDTO>> GetAllAsync();

        Task<IEnumerable<EstablishmentOutDTO>> GetByEstablishmentTypeAsync(Guid typeId);

        Task<IEnumerable<EstablishmentOutDTO>> GetByCityAsync(Guid cityId);

        Task<IEnumerable<EstablishmentOutDTO>> GetByTypeAndCityAsync(Guid typeId, Guid cityId);

        Task AddAsync(EstablishmentInDTO newEstablishment);

        Task UpdateAsync(EstablishmentInDTO updatedEstablishment);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<EstablishmentTypeOutDTO>> GetAllEstablishmentTypesAsync();

    }
}
