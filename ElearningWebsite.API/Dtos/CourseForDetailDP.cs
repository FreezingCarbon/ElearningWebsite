using System;

namespace ElearningWebsite.API.Dtos
{
    public class CourseForDetailDP
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string AvaUrl { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}