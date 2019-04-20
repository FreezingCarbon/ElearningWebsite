using System;
using System.Collections.Generic;

namespace ElearningWebsite.API.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}