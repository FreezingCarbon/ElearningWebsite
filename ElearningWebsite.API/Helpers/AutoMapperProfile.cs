using AutoMapper;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Models;

namespace ElearningWebsite.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {      
            CreateMap<TeacherForUpdateDto, Teacher>();    
            CreateMap<TeacherDetailedDto, Teacher>();

            CreateMap<CourseForAddDto, Course>();
            CreateMap<CourseForUpdateDto, Course>();
            CreateMap<Course, CourseForListDto>();
            CreateMap<Course, CourseForDetailDP>();
            CreateMap<Course, CourseForDetailedDto>();
        }
    }
}