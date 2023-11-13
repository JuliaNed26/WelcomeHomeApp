using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

namespace WelcomeHome.Services.Services
{
    public class SocialPayoutService : ISocialPayoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;

        public SocialPayoutService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
        }

        public async Task AddAsync(SocialPayoutInDTO newPayout)
        {
            var socialPayoutWithSteps = await ConvertDtoIntoEntities(newPayout);
          
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .SocialPayoutRepository
                                                              .AddWithStepsAsync(socialPayoutWithSteps.Item1,
                                                              socialPayoutWithSteps.Item2
                                                              ))
                .ConfigureAwait(false);
            
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SocialPayoutOutDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SocialPayoutOutDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SocialPayoutOutDTO payoutWithUpdateInfo)
        {
            throw new NotImplementedException();
        }

        private async Task<(SocialPayout, Dictionary<int, Step>)> ConvertDtoIntoEntities(SocialPayoutInDTO newPayout)
        {
            var socialPayout = await GenerateSocialPayout(newPayout);

            var steps = GenerateSteps(newPayout.PaymentSteps);

            return (socialPayout, steps);
        }

        private async Task<SocialPayout> GenerateSocialPayout(SocialPayoutInDTO newPayout)
        {
            List<UserCategory> categories = new List<UserCategory>();

            foreach (var categoryId in newPayout.UserCategoriesId)
            {
                var foundCategory = await _unitOfWork.UserCategoryRepository.GetByIdAsync(categoryId);
                if (foundCategory != null)
                {
                    categories.Add(foundCategory);
                }
                else throw new Exception("No such user category");
            }

            SocialPayout socialPayout = new SocialPayout
            {
                Name = newPayout.Name,
                Description = newPayout.Description,
                Amount = newPayout.Amount,
                UserCategories = categories
            };

            return socialPayout;
        }

        private Dictionary<int, Step> GenerateSteps(ICollection<StepInDTO> stepsDTO)
        {
            Dictionary<int, Step> steps = new Dictionary<int, Step>();

            foreach (var step in stepsDTO)
            {
                //пошук існуючих степів в БД. Пошук працює, проте не ясно, як додати всі степи без помилки у бд,
                //не додаючи існуючий,
                //тому поки в коменті

                /*   var foundStep = await _unitOfWork.StepRepository.GetByEstablishmentTypeAndDocuments(step.EstablishmentTypeId, step.DocumentsReceiveId, step.DocumentsBringId);
                   if (foundStep != null)
                   {
                       steps.Add(step.SequenceNumber, foundStep);
                   }
                   else
                   {*/
                var stepEntity = _mapper.Map<Step>(step);

                stepEntity.Id = Guid.NewGuid();
                stepEntity.StepDocuments = new List<StepDocument>();

                stepEntity.StepDocuments.AddRange(GenerateStepDocumentsToReceive(step.DocumentsReceiveId));
                stepEntity.StepDocuments.AddRange(GenerateStepDocumentsToBring(step.DocumentsBringId));

                steps.Add(step.SequenceNumber, stepEntity);
            }
            //}

            return steps;
        }

        private ICollection<StepDocument> GenerateStepDocumentsToReceive(ICollection<Guid> documentsReceive)
        {
            ICollection<StepDocument> stepDocuments = new List<StepDocument>();
            foreach (var document in documentsReceive)
            {
                var newStepDoc = new StepDocument
                {
                    ToReceive = true,
                    DocumentId = document
                };
                stepDocuments.Add(newStepDoc);
            }

            return stepDocuments;
        }

        private ICollection<StepDocument> GenerateStepDocumentsToBring(ICollection<Guid> documentsBring)
        {
            ICollection<StepDocument> stepDocuments = new List<StepDocument>();
            foreach (var document in documentsBring)
            {
                var newStepDoc = new StepDocument
                {
                    ToReceive = false,
                    DocumentId = document
                };
                stepDocuments.Add(newStepDoc);
            }

            return stepDocuments;
        }
    }
}
