using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;

namespace ElearningWebsite.API.Data
{
    public interface IStudentRepository
    {
        Task<bool> RateCourse(int ratingPoint, int courseId, int studentId);
        Task<PagedList<Course>> GetCourses(CourseParam courseParams, int studentId);
        Task<Course> GetCourse(int courseId, int studentId);
        Task<bool> Enroll(int courseId, int studentId);
    }
}