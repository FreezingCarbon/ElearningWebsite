using System;
using System.Web;
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
using Microsoft.AspNetCore.Http;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using System.IO;
using Imgur.API;
using Microsoft.Extensions.Configuration;

namespace ElearningWebsite.API.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public TeacherController(ITeacherRepository repo, IMapper mapper, IConfiguration config)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._config = config;
        }

        [HttpGet("{teacherId}/courses")]
        public async Task<IActionResult> GetCourses([FromQuery]CourseParam courseParam, int teacherId)
        {
            var courses = await _repo.GetCourses(courseParam, teacherId);
            var coursesToReturn = _mapper.Map<IEnumerable<CourseForListDto>>(courses);

            Response.AddPagination(courses.CurrentPage, courses.PageSize, courses.TotalCount, courses.TotalPages);
            
            return Ok(coursesToReturn);
        }

        [HttpGet("{teacherId}/course/{courseId}")]
        public async Task<IActionResult> GetCourse(int teacherId, int courseId)
        {
            var course = await _repo.GetCourse(courseId, teacherId);
            var courseToReturn = _mapper.Map<CourseForDetailedDto>(course);

            return Ok(courseToReturn);
            // var course = await _repo.GetCourse(courseId, teacherId);
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

        [HttpPost("course/{teacherId}")]
        public async Task<IActionResult> AddCourse([FromBody]CourseForAddDto courseForAdd, int teacherId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, teacherId, "Teacher") == false) {
                return Unauthorized();
            }

            var course = _mapper.Map<Course>(courseForAdd);
            var courseToReturn = _mapper.Map<CourseForDetailedDto>(await _repo.AddCourse(course, teacherId));

            return Ok(courseToReturn);
        }

        [HttpPost("{teacherId}/course/{courseId}")]
        public async Task<IActionResult> AddCoursePhoto(int teacherId, int courseId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string
            
            if(AuthHelper.BasicAuth(auth, teacherId, "Teacher") == false) {
                return Unauthorized();
            }

            var files = Request.Form.Files;
            if(files == null) {
                return BadRequest("Request must contain at least a file");
            }
            // init ava and cover file object
            dynamic ava = null;
            dynamic cover = null;
            // condition check the request
            int avaCount = 0;
            int coverCount = 0;
            // file checker init
            FormFileChecker fileTypeChecker = new FormFileChecker();
            foreach(var file in files)
            {
                if(fileTypeChecker.CheckFileType(file, "image")) {
                    if(file.Name == "Ava") {
                        if(avaCount < 1) {
                            ava = file;
                        } else {
                            return BadRequest("Too much Ava to upload");
                        }
                    } else if(file.Name == "Cover") {
                        if(coverCount < 1) {
                            cover = file;
                        } else {
                            return BadRequest("Too much Cover to upload");
                        }
                    } else {
                        return BadRequest("Invalid request. File must be set for Ava or Cover");
                    }
                } else {
                    return BadRequest("Post file must be image type");
                }
            }

            // create FileUploadHelper reference
            FileUploadHelper fileUpload = new FileUploadHelper(_config);
            // Modify image link if posted data != null
            var courseToUpdate = await _repo.GetCourse(courseId, teacherId);
            try {
                if(ava != null) {
                    courseToUpdate.AvaUrl = await fileUpload.UploadImage(ava);
                }
                if(cover != null) {
                    courseToUpdate.CoverUrl = await fileUpload.UploadImage(cover);
                }
            } catch(ImgurException imgurEx) {
                return BadRequest("Unable to upload image to Imgur: " + imgurEx.Message);
            }

            await _repo.SaveAll();
            var courseToReturn = _mapper.Map<CourseForDetailedDto>(courseToUpdate);

            return Ok(courseToReturn);
        }

        [HttpDelete("{teacherId}/course/{courseId}")]
        public async Task<IActionResult> DeleteCourse(int teacherId, int courseId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, teacherId, "Teacher") == false) {
                return Unauthorized();
            }

            if(await _repo.DeleteCourse(courseId)) {
                return Ok();
            } else {
                return BadRequest("This course doesn't exist !!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody]TeacherForUpdateDto teacherForUpdateDto)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, id, "Teacher") == false) {
                return Unauthorized();
            }
            
            var teacherFromRepo = await _repo.GetTeacher(id);
            _mapper.Map(teacherForUpdateDto, teacherFromRepo);

            await _repo.SaveAll();
            var teacherToReturn = _mapper.Map<TeacherDetailedDto>(teacherFromRepo);

            return Ok(teacherToReturn);
        }

        [HttpPut("{teacherId}/course/{courseId}")]
        public async Task<IActionResult> UpdateCourse(int teacherId, int courseId, [FromBody]CourseForUpdateDto courseForUpdate)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, teacherId, "Teacher") == false) {
                return Unauthorized();
            }

            var courseFromRepo = await _repo.GetCourse(courseId, teacherId);
            _mapper.Map(courseForUpdate, courseFromRepo);

            await _repo.SaveAll();
            var courseToReturn = _mapper.Map<CourseForDetailedDto>(courseFromRepo);

            return Ok(courseToReturn);
        }
    }
}