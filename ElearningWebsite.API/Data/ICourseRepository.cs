using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;

namespace ElearningWebsite.API.Data
{
    public interface ICourseRepository
    {
        Task<PagedList<Course>> GetCourses(CourseParam courseParams);
        Task<Course> GetCourse(int courseId);
    }
}