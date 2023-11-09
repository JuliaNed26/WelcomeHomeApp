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

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(UserInDTO newUser)
        { 
            var userEntity = ConvertInDTOIntoEntity(newUser);
            await _unitOfWork.UserRepository.AddAsync(userEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
        }

        public IEnumerable<UserOutDTO> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return users.Select(user => ConvertEntityIntoOutDTO(user));
        }

        public async Task<UserOutDTO> GetAsync(Guid id)
        {
            var foundUser = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (foundUser == null)
                throw new Exception("No user with such id");

            return ConvertEntityIntoOutDTO(foundUser);
        }

        public int GetCount()
        {
            var allUsers = _unitOfWork.UserRepository.GetAll();
            return allUsers.Count();
        }

        public async Task UpdateAsync(UserOutDTO userWithUpdateInfo)
        {
            var userEntity = ConvertOutDTOIntoEntity(userWithUpdateInfo);
            await _unitOfWork.UserRepository.UpdateAsync(userEntity);
        }

        private User ConvertInDTOIntoEntity(UserInDTO dto)
        {
            User entity = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
            };
            return entity;
        }

        private UserOutDTO ConvertEntityIntoOutDTO(User entity)
        {
            UserOutDTO dto = new UserOutDTO
            {
                Id = entity.Id,
                FullName = entity.FullName,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
            };
            return dto;
        }

        private User ConvertOutDTOIntoEntity(UserOutDTO dto)
        {
            User entity = new User
            {
                Id = dto.Id,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
            };
            return entity;
        }
    }
}
