using System;
using System.ComponentModel.DataAnnotations;

namespace ElearningWebsite.API.Dtos
{
    public class TeacherForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 16 character")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public TeacherForRegisterDto()
        {
            this.CreatedAt = DateTime.Now;
        }
        
    }
}