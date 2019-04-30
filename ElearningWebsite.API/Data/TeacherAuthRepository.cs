using System;
using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningWebsite.API.Data
{
    public class TeacherAuthRepository<T> : ITeacherAuthRepository<Teacher>
    {
        private readonly DataContext _context;
        public TeacherAuthRepository(DataContext context)
        {
            this._context = context;

        }
        public async Task<Teacher> Login(string username, string password)
        {
            var user = await _context.Teachers.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
                return null;

            if(!AuthHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<Teacher> Register(Teacher user, string password)
        {
            byte[] passwordHash, passwordSalt;
            AuthHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;   

            await _context.Teachers.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Teachers.AnyAsync(x => x.Username == username))
                return true;
            
            return false;
        }
    }
}