using System;

namespace ElearningWebsite.API.Dtos
{
    public class VideoForDetailedDto
    {
        public int VideoId { get; set; }
        public string VideoUrl { get; set; }
        public string PublicId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}