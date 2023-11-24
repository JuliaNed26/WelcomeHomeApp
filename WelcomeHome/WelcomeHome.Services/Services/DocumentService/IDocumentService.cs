using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IDocumentService
    {
        Task<DocumentOutDTO> GetAsync(Guid id);

        IEnumerable<DocumentOutDTO> GetAll();

        IEnumerable<DocumentOutDTO> GetByStepNeeded(Guid stepId);

        IEnumerable<DocumentOutDTO> GetByStepReceived(Guid stepId);

        Task AddAsync(DocumentInDTO newDocument);

        Task UpdateAsync(DocumentOutDTO updatedDocument);

        Task DeleteAsync(Guid id);
    }
}
