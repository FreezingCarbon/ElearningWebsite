using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ElearningWebsite.API.Data;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElearningWebsite.API.Controllers
{
    [Authorize]
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _repo;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [AllowAnonymous]
        [Route("courses/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery]CourseParam courseParam, int id)
        {
            var courses = await _repo.GetCourses(courseParam, id);
            var coursesToReturn = _mapper.Map<IEnumerable<CourseForListDto>>(courses);

            Response.AddPagination(courses.CurrentPage, courses.PageSize, courses.TotalCount, courses.TotalPages);
            
            return Ok(coursesToReturn); 
        }

        
    }
}