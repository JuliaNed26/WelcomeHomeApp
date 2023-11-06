using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly DataExceptionsHandlerMediator _exceptionHandlerMediator;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper
            /*, Func<Type, DataExceptionsHandlerMediator> exceptionHandlerMediatorFactory*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_exceptionHandlerMediator = exceptionHandlerMediatorFactory(GetType());
        }

        public async Task AddAsync(UserInDTO newUser)
        { 
            var userEntity = _mapper.Map<User>(newUser);
            userEntity.Id = Guid.NewGuid();
            await _unitOfWork.UserRepository.AddAsync(userEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
        }

        public IEnumerable<UserOutDTO> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return users.Select(user => _mapper.Map<UserOutDTO>(user));
        }

        public async Task<UserOutDTO> GetAsync(Guid id)
        {
            var foundUser = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return _mapper.Map<UserOutDTO>(foundUser);
        }

        public int GetCount()
        {
            var allUsers = _unitOfWork.UserRepository.GetAll();
            return allUsers.Count();
        }

        public async Task UpdateAsync(UserInDTO userWithUpdateInfo)
        {
            var userEntity = _mapper.Map<Volunteer>(userWithUpdateInfo);

            await _unitOfWork.VolunteerRepository.UpdateAsync(userEntity);
        }
    }
}
