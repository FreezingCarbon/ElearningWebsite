using System;

namespace ElearningWebsite.API.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        public string VideoUrl { get; set; }
        public string PublicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}