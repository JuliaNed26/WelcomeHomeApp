using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IDocumentService
    {
        Task<DocumentOutDTO> GetAsync(long id);

        IEnumerable<DocumentOutDTO> GetAll();

        IEnumerable<DocumentOutDTO> GetByStepNeeded(long stepId);

        IEnumerable<DocumentOutDTO> GetByStepReceived(long stepId);

        Task AddAsync(DocumentInDTO newDocument);

        Task UpdateAsync(DocumentOutDTO updatedDocument);

        Task DeleteAsync(long id);
    }
}
