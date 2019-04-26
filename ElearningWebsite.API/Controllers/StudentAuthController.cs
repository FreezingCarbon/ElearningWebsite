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
    [Route("api/auth/student")]
    [ApiController]
    public class StudentAuthController : ControllerBase
    {
        private readonly IAuthRepository<Student> _repo;
        private readonly IConfiguration _config;
        public StudentAuthController(IAuthRepository<Student> repo, IConfiguration config)
        {
            this._config = config;
            this._repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(StudentForRegisterDto studentForRegisterDto)
        {
            studentForRegisterDto.Username = studentForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(studentForRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }

            var studentToCreate = new Student
            {
                Username = studentForRegisterDto.Username
            };

            var createdTeacher = await _repo.Register(studentToCreate, studentForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(StudentForLoginDto studentForLoginDto)
        {
            studentForLoginDto.Username = studentForLoginDto.Username.ToLower();
            
            var studentFromRepo = await _repo.Login(studentForLoginDto.Username, studentForLoginDto.Password);

            if (studentFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, studentFromRepo.StudentId.ToString()),
                new Claim(ClaimTypes.Name, studentFromRepo.Username),
                new Claim(ClaimTypes.Role, "Student")
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