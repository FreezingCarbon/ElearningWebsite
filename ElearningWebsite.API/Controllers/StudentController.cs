using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElearningWebsite.API.Data;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ElearningWebsite.API.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpGet("{studentId}/courses/{courseId}")]
        public async Task<IActionResult> GetCourse(int courseId, int studentId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, studentId, "Student") == false) {
                return Unauthorized();
            }

            var enrolled = _repo.IsEnrolled(courseId, studentId);
          
            return Ok(new { enrolled });
        }

        [HttpGet("{studentId}/courses")]
        public async Task<IActionResult> GetCourses([FromQuery]CourseParam courseParams, int studentId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, studentId, "Student") == false) {
                return Unauthorized();
            }

            var courses = await _repo.GetCourses(courseParams, studentId);
            var coursesToReturn = _mapper.Map<IEnumerable<CourseForListDto>>(courses);

            Response.AddPagination(courses.CurrentPage, courses.PageSize, courses.TotalCount, courses.TotalPages);
            
            return Ok(coursesToReturn);
        }
        
        [HttpPost("{studentId}/courses/{courseId}")]
        public async Task<IActionResult> EnrollCourse(int studentId, int courseId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, studentId, "Student") == false) {
                return Unauthorized();
            }

            if(await _repo.Enroll(courseId, studentId)) {
                return Ok();
            } else {
                return BadRequest("Unable to enroll to this course");
            }
        }

        [HttpPut("{studentId}/courses/{courseId}")]
        public async Task<IActionResult> RateCourse(int studentId, int courseId, [FromBody]JObject content)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, studentId, "Student") == false) {
                return Unauthorized();
            }
            
            try {
                var ratingPoint = int.Parse(content.GetValue("ratingPoint").ToString());

                if(await _repo.RateCourse(ratingPoint, courseId, studentId))
                    return Ok();
                else {
                    return StatusCode(304);
                }
            } catch(Exception e) {
                return BadRequest(e.Message);
            }

        }
    }
}