using System.Linq;
using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningWebsite.API.Data
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<Course> GetCourse(int courseId, int teacherId)
        {
            var course = await _context.Courses.Include(c => c.Videos).Where(c => c.TeacherCourses.All(tc => tc.TeacherId == teacherId)).FirstOrDefaultAsync(x => x.CourseId == courseId);

            return course;
        }

        public async Task<PagedList<Course>> GetCourses(CourseParam courseParams, int teacherId)
        {
           var courses = _context.Courses.Where(course => course.TeacherCourses.Any(tc => tc.TeacherId == teacherId));
           
           return await PagedList<Course>.CreateAsync(courses, courseParams.PageNumber, courseParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}