using System.ComponentModel.DataAnnotations;

namespace ElearningWebsite.API.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int Rate { get; set; }
    }
}