using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;

namespace ElearningWebsite.API.Data
{
    public interface ITeacherRepository
    {
        Task<PagedList<Course>> GetCourses(CourseParam courseParams, int teacherId);
        Task<Course> GetCourse(int courseId, int teacherId);
        Task<bool> SaveAll();
    }
}