using System;

namespace ElearningWebsite.API.Dtos
{
    public class CourseForListDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}