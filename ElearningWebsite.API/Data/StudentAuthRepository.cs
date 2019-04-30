using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningWebsite.API.Data
{
    public class StudentAuthRepository<T> : IStudentAuthRepository<Student>
    {
        private readonly DataContext _context;
        public StudentAuthRepository(DataContext context)
        {
            this._context = context;

        }
        public async Task<Student> Login(string username, string password)
        {
            var user = await _context.Students.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
                return null;

            if(!AuthHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<Student> Register(Student user, string password)
        {
            byte[] passwordHash, passwordSalt;
            AuthHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;   

            await _context.Students.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Students.AnyAsync(x => x.Username == username))
                return true;
            
            return false;
        }
    }
}