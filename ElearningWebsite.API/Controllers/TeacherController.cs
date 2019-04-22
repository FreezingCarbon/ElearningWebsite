using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ElearningWebsite.API.Data;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ElearningWebsite.API.Controllers
{
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

        [Route("courses/{teacherId}")]
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery]CourseParam courseParam, int teacherId)
        {
            var courses = await _repo.GetCourses(courseParam, teacherId);
            var coursesToReturn = _mapper.Map<IEnumerable<CourseForListDto>>(courses);

            Response.AddPagination(courses.CurrentPage, courses.PageSize, courses.TotalCount, courses.TotalPages);
            
            return Ok(coursesToReturn);
        }

        [Route("course")]
        [HttpGet]
        public async Task<IActionResult> GetCourse([FromBody]JObject content)
        {
            try {
                var courseId = int.Parse(content.GetValue("courseId").ToString());
                var teacherId = int.Parse(content.GetValue("teacherId").ToString());

                var course = await _repo.GetCourse(courseId, teacherId);
                var courseToReturn = _mapper.Map<CourseForDetailedDto>(course);

                return Ok(courseToReturn);
            } catch(Exception e) {
                return BadRequest("Invalid Course Id and Teacher Id: \n" + e.ToString());
            }
            // var course = await _repo.GetCourse(courseId, teacherId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody]TeacherForUpdateDto teacherForUpdateDto)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string
            if(auth == null) { 
                return Unauthorized();
            }

            AuthHelper authCheck = new AuthHelper();
            if(authCheck.BasicAuth(auth, id, "Teacher") == false) {
                return Unauthorized();
            }
            
            var teacherFromRepo = await _repo.GetTeacher(id);
            _mapper.Map(teacherForUpdateDto, teacherFromRepo);

            await _repo.SaveAll();
            var teacherToReturn = _mapper.Map<TeacherDetailedDto>(teacherFromRepo);

            return Ok(teacherToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacherFromRepo = await _repo.GetTeacher(id);

            if(teacherFromRepo == null)
                return BadRequest("This teacher doesn't exist");
            
            var teacherToReturn = _mapper.Map<TeacherDetailedDto>(teacherFromRepo);
            return Ok(teacherToReturn);
        }

        [HttpPut("course/{teacherId}")]
        public async Task<IActionResult> AddCourse([FromBody]CourseForAddDto courseForAdd, int teacherId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string
            if(auth == null) {
                return Unauthorized();
            }

            AuthHelper authCheck = new AuthHelper();
            if(authCheck.BasicAuth(auth, teacherId, "Teacher") == false) {
                return Unauthorized();
            }

            var course = _mapper.Map<Course>(courseForAdd);
            var courseToReturn = _mapper.Map<CourseForDetailedDto>(await _repo.AddCourse(course, teacherId));

            return Ok(courseToReturn);
        }

        [HttpDelete("course/{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromBody]JObject content, int courseId)
        {
            try {
                var teacherId = int.Parse(content.GetValue("teacherId").ToString());

                string auth = Request.Headers["Authorization"]; // get bearer string
                if(auth == null) {
                    return Unauthorized();
                }

                AuthHelper authCheck = new AuthHelper();
                if(authCheck.BasicAuth(auth, teacherId, "Teacher") == false) {
                    return Unauthorized();
                }

                if(await _repo.DeleteCourse(courseId)) {
                    return Ok();
                } else {
                    return BadRequest("This course doesn't exist !!");
                }

            } catch(Exception e) {
                return BadRequest("Invalid Course Id and Teacher Id: \n" + e.ToString());
            }
        }
    }
}