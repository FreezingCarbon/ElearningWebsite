using System.Linq;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Threading.Tasks;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ElearningWebsite.API.Data
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _context;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public TeacherRepository(DataContext context, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._context = context;
            this._cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<Course> AddCourse(Course courseToAdd, int teacherId)
        {
            _context.Courses.Add(courseToAdd);
            _context.SaveChanges();
            var courseId = courseToAdd.CourseId;
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == teacherId);
            _context.TeacherCourses.Add(new TeacherCourse {Teacher = teacher, Course = courseToAdd, TeacherId = teacherId, CourseId = courseId});
            _context.SaveChanges();
            return await _context.Courses.FirstOrDefaultAsync(course => course.CourseId == courseId);
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            var courseToDelete = await _context.Courses.FirstOrDefaultAsync(course => course.CourseId == courseId);
            
            if(courseToDelete == null) {
                return false;
            }

            var videos = await _context.Videos.Where(v => v.CourseId == courseId).ToListAsync();
            foreach (var video in videos)
            {
                if(video.PublicId != null) {
                    var delParam = new DeletionParams(video.PublicId)
                    {
                        ResourceType = ResourceType.Video
                    };

                    var delResResult = _cloudinary.Destroy(delParam);
                }
            }
            _context.Courses.Remove(courseToDelete);
            await _context.SaveChangesAsync();

            return true;
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

        public async Task<Teacher> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == id);

            return teacher;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}