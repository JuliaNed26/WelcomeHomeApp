using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services.UserCategoryService
{
    public interface IUserCategoryService
    {
        Task<UserCategoryOutDTO> GetAsync(long id);
        IEnumerable<UserCategoryOutDTO> GetAll();
    }
}
