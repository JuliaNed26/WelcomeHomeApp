using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();

        Task<Course?> GetByIdAsync(int id);

        Task AddAsync(Course newCourse);

        Task DeleteAsync(int id);

        Task UpdateAsync(Course editedCourse);
    }
}
