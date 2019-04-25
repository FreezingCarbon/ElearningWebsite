using System;

namespace ElearningWebsite.API.Dtos
{
    public class VideoForCreateDto
    {
        public string VideoUrl { get; set; }
        public string PublicId { get; set; }
        public int CourseId { get; set; }
        public DateTime CreatedAt { get; set; }

        public VideoForCreateDto()
        {
            CreatedAt = DateTime.Now;
        }
    }
}