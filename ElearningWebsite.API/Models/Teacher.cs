using System;
using System.Collections.Generic;

namespace ElearningWebsite.API.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Certificate { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<TeacherCourse> TeacherCourses { get; set; }

    }
}