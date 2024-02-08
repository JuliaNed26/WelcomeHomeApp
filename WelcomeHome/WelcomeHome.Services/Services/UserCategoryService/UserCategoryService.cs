using AutoMapper;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;

namespace WelcomeHome.Services.Services.UserCategoryService
{
    public class UserCategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserCategoryOutDTO> GetAsync(long id)
        {
            var foundCategory = await _unitOfWork.UserCategoryRepository.GetByIdAsync(id).ConfigureAwait(false);
            return foundCategory == null
                ? throw new RecordNotFoundException("User category was not found")
                : _mapper.Map<UserCategoryOutDTO>(foundCategory);
        }

        public IEnumerable<UserCategoryOutDTO> GetAll()
        {
            return _unitOfWork.UserCategoryRepository.GetAll()
                                                      .Select(e => _mapper.Map<UserCategoryOutDTO>(e));
        }
    }
}
