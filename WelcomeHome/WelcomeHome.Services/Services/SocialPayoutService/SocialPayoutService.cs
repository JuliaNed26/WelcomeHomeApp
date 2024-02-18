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
            var socialPayout = await ConvertDtoIntoEntities(newPayout);

            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .SocialPayoutRepository.AddAsync(socialPayout))
                                                              .ConfigureAwait(false);

        }

        public async Task DeleteAsync(int id)
        {
            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork.SocialPayoutRepository.DeleteAsync(id))
                                   .ConfigureAwait(false);
        }

        public IEnumerable<SocialPayoutListItemDTO> GetAll()
        {
            var allSocialPayouts = _unitOfWork.SocialPayoutRepository.GetAll().ToList();
            List<SocialPayoutListItemDTO> dtos = new List<SocialPayoutListItemDTO>();
            foreach (var payment in allSocialPayouts)
            {
                dtos.Add(_mapper.Map<SocialPayoutListItemDTO>(payment));
            }
            return dtos;
        }

        public async Task<SocialPayoutOutDTO> GetAsync(int id)
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

        public async Task UpdateAsync(SocialPayoutOutDTO payoutWithUpdateInfo)
        {
            var newSocialPayout  = _mapper.Map<SocialPayout>(payoutWithUpdateInfo);


            await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
                                                              .SocialPayoutRepository
                                                              .UpdateAsync(newSocialPayout)
                                                              )
                .ConfigureAwait(false);
        }



        private async Task<SocialPayout> ConvertDtoIntoEntities(SocialPayoutInDTO newPayout)
        {
            var socialPayout = await GenerateSocialPayout(newPayout);

            return socialPayout;
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
                PaymentSteps = GeneratePaymentStepsforExistingSteps(newPayout.PaymentSteps)
            };
            return socialPayout;
        }

        private List<PaymentStep> GeneratePaymentStepsforExistingSteps(ICollection<ExistingStepInDTO> steps)
        {
            List<PaymentStep> newTables = new List<PaymentStep>();
            foreach (var step in steps)
            {
                PaymentStep newPaymentStep = new PaymentStep
                {
                    StepId = step.StepId,
                    SequenceNumber = step.SequenceNumber
                };
                newTables.Add(newPaymentStep);
            }
            return newTables;

        }

        private SocialPayoutOutDTO ConvertEntityIntoOutDTO(SocialPayout entity)
        {
            SocialPayoutOutDTO dto = new SocialPayoutOutDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Amount = entity.Amount,
                UserCategories = entity.UserCategories != null ? _mapper.Map<List<UserCategoryOutDTO>>(entity.UserCategories) : null,
            };
            return dto;
        }
    }
}