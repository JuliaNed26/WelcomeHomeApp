using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IUserService
    {
        int GetCount();

        Task<UserOutDTO> GetAsync(Guid id);

        IEnumerable<UserOutDTO> GetAll();

        Task AddAsync(UserInDTO newUser);

        Task UpdateAsync(UserInDTO userWithUpdateInfo);

        Task DeleteAsync(Guid id);
    }
}
