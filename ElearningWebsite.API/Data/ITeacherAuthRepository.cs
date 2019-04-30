using System.Threading.Tasks;

namespace ElearningWebsite.API.Data
{
    public interface ITeacherAuthRepository<T>
    {
        Task<T> Register(T user, string password);
        Task<T> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}