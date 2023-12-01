using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IDocumentService
    {
        Task<DocumentOutDTO> GetAsync(int id);

        IEnumerable<DocumentOutDTO> GetAll();

        IEnumerable<DocumentOutDTO> GetByStepNeeded(int stepId);

        IEnumerable<DocumentOutDTO> GetByStepReceived(int stepId);

        Task AddAsync(DocumentInDTO newDocument);

        Task UpdateAsync(DocumentOutDTO updatedDocument);

        Task DeleteAsync(int id);
    }
}
