using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;
using WelcomeHome.Services.Exceptions;

namespace WelcomeHome.Services.Services
{
    public class StepService :IStepService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;

        public StepService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
        }

        public async Task AddAsync(StepInDTO newStep)
        {
            var step = _mapper.Map<Step>(newStep);
            await AddDocumentStepsAsync(newStep, step);

            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .StepRepository
                                                              .AddAsync(step))
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork.StepRepository.DeleteAsync(id))
                                   .ConfigureAwait(false);
        }

        public async Task<IEnumerable<StepOutDTO>> GetAllAsync()
        {
            var steps = _unitOfWork.StepRepository.GetAll().ToList();
            var result = new List<StepOutDTO>();
            foreach (var step in steps)
            {
                var stepOut = _mapper.Map<StepOutDTO>(step);
                AttachEstablishmentsToDTO(stepOut, step.EstablishmentTypeId);
                await AttachDocumentsBringToDTOAsync(step, stepOut);
                await AttachDocumentsReceiveToDTOAsync(step, stepOut);
                result.Add(stepOut);
            }

            return result;
        }

        public async Task<StepOutDTO> GetAsync(Guid id)
        {
            var foundStep = await _unitOfWork.StepRepository.GetByIdAsync(id);
            if (foundStep == null)
            {
                throw new RecordNotFoundException("No step with such id");
            }

            var stepOut = _mapper.Map<StepOutDTO>(foundStep);
            AttachEstablishmentsToDTO(stepOut, foundStep.EstablishmentTypeId);
            await AttachDocumentsBringToDTOAsync(foundStep, stepOut);
            await AttachDocumentsReceiveToDTOAsync(foundStep, stepOut);

            return stepOut;
        }

        public async Task<IEnumerable<StepOutDTO>> GetByEstablishmentTypeIdAsync(Guid establishmentTypeId)
        {
            var steps = _unitOfWork.StepRepository.GetAll().Where(e=>e.EstablishmentTypeId==establishmentTypeId);

            var result = new List<StepOutDTO>();
            foreach (var step in steps)
            {
                var stepOut = _mapper.Map<StepOutDTO>(step);
                AttachEstablishmentsToDTO(stepOut, establishmentTypeId);
                await AttachDocumentsBringToDTOAsync(step, stepOut);
                await AttachDocumentsReceiveToDTOAsync(step, stepOut);
                result.Add(stepOut);
            }

            return result;
        }

        public async Task UpdateAsync(StepOutDTO stepWithUpdateInfo)
        {
            var stepEntity = _mapper.Map<Step>(stepWithUpdateInfo);

            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .StepRepository
                                                              .UpdateAsync(stepEntity))
                .ConfigureAwait(false);
        }

        private async Task AttachDocumentsReceiveToDTOAsync(Step step, StepOutDTO stepOut)
        {
            var docs = new List<DocumentOutDTO>();
            foreach (var stepDocument in step.StepDocuments)
            {
                if (stepDocument.ToReceive)
                {
                    var document = await _unitOfWork.DocumentRepository.GetByIdAsync(stepDocument.DocumentId);
                    if (document != null)
                    {
                        docs.Add(_mapper.Map<DocumentOutDTO>(document));
                    }
                }
            }
            if (docs.Any())
            {
                stepOut.DocumentsReceive = docs;
            }
        }
        private async Task AttachDocumentsBringToDTOAsync(Step step, StepOutDTO stepOut)
        {
            var docs = new List<DocumentOutDTO>();
            foreach (var stepDocument in step.StepDocuments)
            {
                if (!stepDocument.ToReceive)
                {
                    var document = await _unitOfWork.DocumentRepository.GetByIdAsync(stepDocument.DocumentId);
                    if (document != null)
                    {
                        docs.Add(_mapper.Map<DocumentOutDTO>(document));

                    }
                }
            }
            if (docs.Any())
            {
                stepOut.DocumentsBring=docs;
            }
        }

        private void AttachEstablishmentsToDTO(StepOutDTO step, Guid establishmentTypeId)
        {
            var establishments = _unitOfWork.EstablishmentRepository.GetAll().ToList();
            var stepEstablishments = establishments.Where(e => e.EstablishmentTypeId == establishmentTypeId);
            if (stepEstablishments.Any())
            {
                step.Establishments = _mapper.Map<List<EstablishmentOutDTO>>(stepEstablishments);
            }
        }

        private async Task AddDocumentStepsAsync(StepInDTO stepIn, Step step)
        {
            var stepDocuments = new List<StepDocument>();
            foreach (var documentId in stepIn.DocumentsReceiveId)
            {
                var stepDocument = new StepDocument()
                {
                    DocumentId = documentId,
                    StepId = step.Id,
                    ToReceive = true
                };
                stepDocuments.Add(stepDocument);
            }

            foreach (var documentId in stepIn.DocumentsBringId)
            {
                var stepDocument = new StepDocument()
                {
                    DocumentId = documentId,
                    StepId = step.Id,
                    ToReceive = false
                };
                stepDocuments.Add(stepDocument);
            }

            step.StepDocuments = stepDocuments;
        }
    }
}
