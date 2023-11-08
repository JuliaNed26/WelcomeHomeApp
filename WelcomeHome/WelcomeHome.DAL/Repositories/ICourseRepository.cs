using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();

        Task<Course?> GetByIdAsync(Guid id);

        Task Add(Course newCourse);

        Task Delete(Guid id);

        Task Update(Course editedCourse);
    }
}
