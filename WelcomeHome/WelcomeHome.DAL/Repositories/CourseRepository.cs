using WelcomeHome.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WelcomeHome.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly WelcomeHomeDbContext _context;

        public CourseRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.Select(e => e);
        }

        public async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _context.Courses.FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
        }

        public async Task AddAsync(Course newCourse)
        {
            await _context.Courses.AddAsync(newCourse).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingCourse = await _context.Courses.SingleAsync(c => c.Id == id).ConfigureAwait(false);
            _context.Courses.Remove(existingCourse);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(Course editedCourse)
        {
            _context.Courses.Update(editedCourse);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}