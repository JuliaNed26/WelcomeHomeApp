using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();

        Task<Course?> GetByIdAsync(long id);

        Task AddAsync(Course newCourse);

        Task DeleteAsync(long id);

        Task UpdateAsync(Course editedCourse);
    }
}
