using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Exceptions.ExceptionHandlerMediator;

namespace WelcomeHome.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ExceptionHandlerMediatorBase _exceptionHandler;

		public UserService(IUnitOfWork unitOfWork, IMapper mapper, ExceptionHandlerMediatorBase exceptionHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
        }

        public async Task AddAsync(UserInDTO newUser)
        {
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .UserRepository
			                                                  .AddAsync(_mapper.Map<User>(newUser)))
		        .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .UserRepository
			                                                  .DeleteAsync(id))
		        .ConfigureAwait(false);
        }

        public IEnumerable<UserOutDTO> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return users.Select(user => _mapper.Map<UserOutDTO>(user));
        }

        public async Task<UserOutDTO> GetAsync(Guid id)
        {
	        var foundUser = await _unitOfWork.UserRepository.GetByIdAsync(id);
	        return foundUser == null
		        ? throw new RecordNotFoundException("No user with such id")
		        : _mapper.Map<UserOutDTO>(foundUser);
        }

        public int GetCount()
        {
            var allUsers = _unitOfWork.UserRepository.GetAll();
            return allUsers.Count();
        }

        public async Task UpdateAsync(UserOutDTO userWithUpdateInfo)
        {
	        await _exceptionHandler.HandleAndThrowAsync(() => _unitOfWork
			                                                  .UserRepository
			                                                  .UpdateAsync(_mapper.Map<User>(userWithUpdateInfo)))
		        .ConfigureAwait(false);
        }
    }
}
