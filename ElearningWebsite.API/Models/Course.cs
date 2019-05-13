using System;
using System.Collections.Generic;

namespace ElearningWebsite.API.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string AvaUrl { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<TeacherCourse> TeacherCourses { get; set; }
        public ICollection<Video> Videos { get; set; }

    }
}