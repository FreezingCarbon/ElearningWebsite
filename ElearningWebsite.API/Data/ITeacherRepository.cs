using System.Threading.Tasks;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;

namespace ElearningWebsite.API.Data
{
    public interface ITeacherRepository
    {
        Task<PagedList<Course>> GetCourses(CourseParam courseParams, int teacherId);
        Task<Course> GetCourse(int courseId, int teacherId);
        Task<Teacher> GetTeacher(int id);
        Task<bool> SaveAll();
        Task<Course> AddCourse(Course courseToAdd, int teacherId);
        Task<bool> DeleteCourse(int courseId);
    }
}