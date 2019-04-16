using System.Collections.Generic;

namespace ElearningWebsite.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CourseCategory> CourseCategories { get; set; }
    }
}