using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningWebsite.API.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<Course> GetCourse(int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == courseId);

            return course;
        }

        public async Task<PagedList<Course>> GetCourses(CourseParam courseParams)
        {
            var courses = _context.Courses;
            
            return await PagedList<Course>.CreateAsync(courses, courseParams.PageNumber, courseParams.PageSize);
        }
    }
}