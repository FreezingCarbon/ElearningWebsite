using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ElearningWebsite.API.Data;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ElearningWebsite.API.Controllers
{
    [Route("api/auth/teacher")]
    [ApiController]
    public class TeacherAuthController : ControllerBase
    {
        private readonly IAuthRepository<Teacher> _repo;
        private readonly IConfiguration _config;
        public TeacherAuthController(IAuthRepository<Teacher> repo, IConfiguration config)
        {
            this._config = config;
            this._repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(TeacherForRegisterDto teacherForRegisterDto)
        {
            teacherForRegisterDto.Username = teacherForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(teacherForRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }

            var teacherToCreate = new Teacher
            {
                Username = teacherForRegisterDto.Username
            };

            var createdTeacher = await _repo.Register(teacherToCreate, teacherForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(TeacherForLoginDto teacherForLoginDto)
        {
            teacherForLoginDto.Username = teacherForLoginDto.Username.ToLower();
            
            var teacherFromRepo = await _repo.Login(teacherForLoginDto.Username, teacherForLoginDto.Password);

            if (teacherFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, teacherFromRepo.TeacherId.ToString()),
                new Claim(ClaimTypes.Name, teacherFromRepo.Username),
                new Claim(ClaimTypes.GivenName, "Teacher")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}