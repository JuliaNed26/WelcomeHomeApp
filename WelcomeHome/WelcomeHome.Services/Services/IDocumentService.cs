using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IDocumentService
    {
        Task<DocumentOutDTO> GetAsync(Guid id);

        IEnumerable<DocumentOutDTO> GetAll();

        Task AddAsync(DocumentInDTO newDocument);

        Task UpdateAsync(DocumentInDTO updatedDocument);

        Task DeleteAsync(Guid id);
    }
}
