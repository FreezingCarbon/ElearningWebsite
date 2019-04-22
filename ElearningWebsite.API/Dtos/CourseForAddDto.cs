using System;
using System.ComponentModel.DataAnnotations;

namespace ElearningWebsite.API.Dtos
{
    public class CourseForAddDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public DateTime CreatedAt { get; set; }
        public CourseForAddDto()
        {
            CreatedAt = DateTime.Now;
        }
    }
}