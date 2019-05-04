using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ElearningWebsite.API.Data;
using ElearningWebsite.API.Dtos;
using ElearningWebsite.API.Helpers;
using ElearningWebsite.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ElearningWebsite.API.Controllers
{
    [Route("api/teacher/{teacherId}/courses/{courseId}/videos/")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public VideoController(IVideoRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddVideo(int courseId, int teacherId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, teacherId, "Teacher") == false) {
                return Unauthorized();
            }

            var files = Request.Form.Files;

            if(files == null) {
                return BadRequest("Request must contain video file");
            }
            
            FormFileChecker fileChecker = new FormFileChecker();
            foreach(var file in files) {
                if(!fileChecker.CheckFileType(file, "video")) {
                    return BadRequest("Course Video must be type of video");
                }
            }

            // return Ok(files);
            
            var videoToReturn = new List<VideoForDetailedDto>();
            FileUploadHelper fileUploadHelper = new FileUploadHelper(_cloudinary);
            foreach(var file in files) {
                var uploadResult = fileUploadHelper.UploadVideo(file);
                var videoToCreate = new VideoForCreateDto
                {
                    CourseId = courseId,
                    VideoUrl = uploadResult.Uri.ToString(),
                    PublicId = uploadResult.PublicId
                };
                var videoToAdd = _mapper.Map<Models.Video>(videoToCreate);
                var videoAdded = await _repo.AddVideo(videoToAdd, courseId);
                await _repo.SaveAll();
                videoToReturn.Add(_mapper.Map<VideoForDetailedDto>(videoAdded));
            }
            
            return Ok(videoToReturn);
        }

        [HttpDelete("{videoId}")]
        public async Task<IActionResult> DeleteVideo(int teacherId, int videoId)
        {
            string auth = Request.Headers["Authorization"]; // get bearer string

            if(AuthHelper.BasicAuth(auth, teacherId, "Teacher") == false) {
                return Unauthorized();
            }

            var videoToDelete = await _repo.GetVideo(videoId);

            if(videoToDelete != null) {
                if(videoToDelete.PublicId != null) {
                    var delParam = new DeletionParams(videoToDelete.PublicId)
                    {
                        ResourceType = ResourceType.Video
                    };

                    var delResResult = _cloudinary.Destroy(delParam);
                }

                await _repo.DeleteVideo(videoId);

                return Ok();
            } else {
                return BadRequest("This course doesn't exist !!");
            }
        }
    }
}