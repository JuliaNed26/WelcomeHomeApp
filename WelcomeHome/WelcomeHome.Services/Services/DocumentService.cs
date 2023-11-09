using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public sealed class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //TODO: AddAsync exceptions
        //TODO: AddAsync stedDocument repository
        //TODO: AddAsync getByStep methods

        public async Task<DocumentOutDTO> GetAsync(Guid id)
        {
            var foundDocument = await _unitOfWork.DocumentRepository.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<DocumentOutDTO>(foundDocument);
        }

        public IEnumerable<DocumentOutDTO> GetAll()
        {
            return _unitOfWork.DocumentRepository.GetAll()
                                                 .Select(d => _mapper.Map<DocumentOutDTO>(d));
        }

        public IEnumerable<DocumentOutDTO> GetByStepNeeded(Guid stepId)
        {
            var byStep = _unitOfWork.DocumentRepository.GetAll()
                                                       .Where(d => d.StepDocuments
                                                       .Any(ds => ds.StepId == stepId && ds.ToReceive == false));
            return byStep.Select(d => _mapper.Map<DocumentOutDTO>(d));
        }

        public IEnumerable<DocumentOutDTO> GetByStepReceived(Guid stepId)
        {
            var byStep = _unitOfWork.DocumentRepository.GetAll()
                                                       .Where(d => d.StepDocuments
                                                       .Any(ds => ds.StepId == stepId && ds.ToReceive == true));
            return byStep.Select(d => _mapper.Map<DocumentOutDTO>(d));
        }

        public async Task AddAsync(DocumentInDTO newDocument)
        {
            await _unitOfWork.DocumentRepository.AddAsync(_mapper.Map<Document>(newDocument)).ConfigureAwait(false);
        }

        public async Task UpdateAsync(DocumentInDTO updatedDocument)
        {
            await _unitOfWork.DocumentRepository.UpdateAsync(_mapper.Map<Document>(updatedDocument)).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.DocumentRepository.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}
