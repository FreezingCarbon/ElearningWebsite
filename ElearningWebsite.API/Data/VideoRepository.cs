using System.Linq;
using System.Threading.Tasks;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningWebsite.API.Data
{
    public class VideoRepository : IVideoRepository
    {
        private readonly DataContext _context;

        public VideoRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<Video> AddVideo(Video videoToAdd, int courseId)
        {
            videoToAdd.CourseId = courseId;
            _context.Videos.Add(videoToAdd);
            await _context.SaveChangesAsync();

            return await _context.Videos.FirstOrDefaultAsync(v => v == videoToAdd);
        }

        public async Task<bool> DeleteVideo(int videoId)
        {
            var videoToDelete = await _context.Videos.FirstOrDefaultAsync(v => v.VideoId == videoId);

            if(videoToDelete == null) {
                return false;
            }

            _context.Videos.Remove(videoToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Video> GetVideo(int videoId)
        {
            var videoFromRepo = await _context.Videos.FirstOrDefaultAsync(v => v.VideoId == videoId);

            return videoFromRepo;
        }

        public async Task<PagedList<Video>> GetVideos(VideoParam videoParams, int courseId)
        {
            var videoFromRepo = _context.Videos.Where(v => v.CourseId == courseId);

            return await PagedList<Video>.CreateAsync(videoFromRepo, videoParams.PageNumber, videoParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}