using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services.UserCategoryService
{
    public interface IUserCategoryService
    {
        Task<UserCategoryOutDTO> GetAsync(int id);
        IEnumerable<UserCategoryOutDTO> GetAll();
    }
}
