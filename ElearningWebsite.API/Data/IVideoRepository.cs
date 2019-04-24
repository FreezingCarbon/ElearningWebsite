using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;

namespace ElearningWebsite.API.Data
{
    public interface IVideoRepository
    {
        Task<PagedList<Video>> GetVideos(VideoParam videoParams, int courseId);
        Task<Video> GetVideo(int videoId);
        Task<bool> SaveAll();
        Task<Video> AddVideo(Video videoToAdd, int courseId);
        Task<bool> DeleteVideo(int videoId);
    }
}