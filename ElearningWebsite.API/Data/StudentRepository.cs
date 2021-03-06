using System.Linq;
using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningWebsite.API.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<bool> Enroll(int courseId, int studentId)
        {
            var enrolled = new StudentCourse 
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.StudentCourses.Add(enrolled);

            return await _context.SaveChangesAsync() > 0;
        }

        public bool IsEnrolled(int courseId, int studentId)
        {
            var enrolled = _context.StudentCourses.Where(sc => (sc.CourseId == courseId) && (sc.StudentId == studentId)).Count() > 0;
            return enrolled;
        }

        public async Task<PagedList<Course>> GetCourses(CourseParam courseParams, int studentId)
        {
            var courses = _context.Courses.Where(course => course.StudentCourses.Any(sc => sc.StudentId == studentId));

            return await PagedList<Course>.CreateAsync(courses, courseParams.PageNumber, courseParams.PageSize);
        }

        public async Task<bool> RateCourse(int ratingPoint, int courseId, int studentId)
        {
            var scToUpdate = await _context.StudentCourses.FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            scToUpdate.Rate = ratingPoint;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}