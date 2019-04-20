using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElearningWebsite.API.Data;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElearningWebsite.API.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        private readonly ICourseRepository _repo;
        private readonly IMapper _mapper;

        public AnonymousController(ICourseRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourses([FromQuery]CourseParam courseParam)
        {
            var courses = await _repo.GetCourses(courseParam);
            var coursesToReturn = _mapper.Map<IEnumerable<CourseForListDto>>(courses);
            Response.AddPagination(courses.CurrentPage, courses.PageSize, courses.TotalCount, courses.TotalPages);
            
            return Ok(coursesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _repo.GetCourse(id);
            var courseToReturn = _mapper.Map<CourseForDetailDP>(course);

            return Ok(courseToReturn);
        }
    }
}