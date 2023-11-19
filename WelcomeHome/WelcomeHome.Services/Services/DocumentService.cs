using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

namespace WelcomeHome.Services.Services
{
    public sealed class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<DocumentOutDTO> GetAsync(Guid id)
        {
            var foundDocument = await _unitOfWork.DocumentRepository.GetByIdAsync(id).ConfigureAwait(false);
            return foundDocument == null
	            ? throw new RecordNotFoundException("Document was not found")
	            : _mapper.Map<DocumentOutDTO>(foundDocument);
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
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .DocumentRepository
		                                                      .AddAsync(_mapper.Map<Document>(newDocument)))
		        .ConfigureAwait(false);
        }

        public async Task UpdateAsync(DocumentOutDTO updatedDocument)
        {
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .DocumentRepository
			                                                  .UpdateAsync(_mapper.Map<Document>(updatedDocument)))
		        .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
	                                                          .DocumentRepository
	                                                          .DeleteAsync(id))
	            .ConfigureAwait(false);
        }
    }
}
