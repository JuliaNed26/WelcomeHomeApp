using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WelcomeHome.DAL.Models;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(UserInDTO newUser)
        { 
            await _unitOfWork.UserRepository.AddAsync(_mapper.Map<User>(newUser));
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
	        return foundUser == null
		        ? throw new Exception("No user with such id")
		        : _mapper.Map<UserOutDTO>(foundUser);
        }

        public int GetCount()
        {
            var allUsers = _unitOfWork.UserRepository.GetAll();
            return allUsers.Count();
        }

        public async Task UpdateAsync(UserOutDTO userWithUpdateInfo)
        {
            await _unitOfWork.UserRepository.UpdateAsync(_mapper.Map<User>(userWithUpdateInfo));
        }
    }
}
