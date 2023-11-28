using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

namespace WelcomeHome.Services.Services
{
    public class SocialPayoutService : ISocialPayoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;
        private readonly IStepService _stepService;

        public SocialPayoutService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler, IStepService stepService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
            _stepService = stepService;
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

        public async Task DeleteAsync(Guid id)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork.SocialPayoutRepository.DeleteAsync(id))
                                   .ConfigureAwait(false);
        }

        public IEnumerable<SocialPayoutOutDTO> GetAll()
        {
            var allSocialPayouts = _unitOfWork.SocialPayoutRepository.GetAll().ToList();
            List<SocialPayoutOutDTO> dtos = new List<SocialPayoutOutDTO>();
            foreach (var payment in allSocialPayouts)
            {
                dtos.Add(ConvertEntityIntoOutDTO(payment));
            }
            return dtos;
        }

        public async Task<SocialPayoutOutDTO> GetAsync(Guid id)
        {
            var socialPayout = await _unitOfWork.SocialPayoutRepository.GetByIdAsync(id);
            if (socialPayout == null)
            {
                throw new RecordNotFoundException("No socialPayout with such id");
            }

            
            var socialPayoutDto = ConvertEntityIntoOutDTO(socialPayout);
            socialPayoutDto.Steps = new List<StepOutDTO>();

            foreach (var step in socialPayout.PaymentSteps)
            {
                var step_dto = await _stepService.GetAsync(step.StepId);
                socialPayoutDto.Steps.Add(step_dto);
            }

            return socialPayoutDto;
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(SocialPayoutInDTO payoutWithUpdateInfo)
        {
            var socialPayoutWithSteps = await ConvertDtoIntoEntities(payoutWithUpdateInfo);
            socialPayoutWithSteps.Item1.Id = (Guid)payoutWithUpdateInfo.Id;

            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .SocialPayoutRepository
                                                              .UpdateWithStepsAsync(socialPayoutWithSteps.Item1,
                                                              socialPayoutWithSteps.Item2
                                                              ))
                .ConfigureAwait(false);
        }



        private async Task<(SocialPayout, Dictionary<int, Step>)> ConvertDtoIntoEntities(SocialPayoutInDTO newPayout)
        {
            var socialPayout = await GenerateSocialPayout(newPayout);

            
            var steps = newPayout.NewPaymentSteps == null ? null : GenerateSteps(newPayout.NewPaymentSteps);


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
                UserCategories = categories,
                PaymentSteps = newPayout.ExistingPaymentSteps == null ? null : GeneratePaymentStepsforExistingSteps(newPayout.ExistingPaymentSteps)
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

        private List<PaymentStep> GeneratePaymentStepsforExistingSteps(ICollection<ExistingStepInDTO> steps)
        {
                List<PaymentStep> newTables = new List<PaymentStep>();
                foreach (var step in steps)
                {
                    PaymentStep newPaymentStep = new PaymentStep
                    {
                        StepId = step.stepId,
                        SequenceNumber = step.SequenceNumber
                    };
                    newTables.Add(newPaymentStep);
                }
                return newTables;
            
        }

        private SocialPayoutOutDTO ConvertEntityIntoOutDTO(SocialPayout entity){

            
            SocialPayoutOutDTO dto = new SocialPayoutOutDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Amount = entity.Amount,
                UserCategoriesId = entity.UserCategories != null ? GetUserCategories(entity.UserCategories) : null,
            };
            return dto;
        }

        private List<Guid> GetUserCategories(ICollection<UserCategory> categories)
        {
            List<Guid> userCategoryIds = new List<Guid>();
            foreach (var category in categories)
            {
                userCategoryIds.Add(category.Id);
            }
            return userCategoryIds;
        }
    }
}
