using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Imgur.API;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace ElearningWebsite.API.Helpers
{
    public class FileUploadHelper
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;

        public FileUploadHelper(Cloudinary cloudinary)
        {
            this._cloudinary = cloudinary;
        }

        public FileUploadHelper(IConfiguration config)
        {
            this._config = config;
        }
        public async Task<string> UploadImage(IFormFile image)
        {
            try {
                var clientId = _config.GetSection("ImgurSettings").GetValue<string>("ClientId");
                var clientSecret = _config.GetSection("ImgurSettings").GetValue<string>("ClientSecret");

                var client = new ImgurClient(clientId, clientSecret);
                var endpoint = new ImageEndpoint(client);
                if(image.Length > 0) {
                    var fileStream = image.OpenReadStream();
                
                
                    IImage uploadImage = await endpoint.UploadImageStreamAsync(fileStream);

                    return uploadImage.Link;
                } else {
                    throw new ImgurException("Image size = 0");
                }
                

            } catch (ImgurException imgurEx) {
                throw imgurEx;
            }
        }

        public VideoUploadResult UploadVideo(IFormFile file)
        {
            try {
                if(file.Length > 0) {
                    var fileStream = file.OpenReadStream();

                    var uploadParams = new VideoUploadParams()
                    {
                        File = new FileDescription(file.FileName, fileStream),
                        Overwrite = true,
                        PublicId = "ElearningWebsiteVideos/" + file.FileName.Replace(" ", ""),
                        EagerAsync = true,
                        EagerTransforms = new List<Transformation>() 
                        {
                            new Transformation().Height(360).Width(480).Quality(720)
                        }       
                    };
                    var uploadResult = _cloudinary.UploadLarge(uploadParams);

                    return uploadResult;
                } else {
                    throw new Exception("Video upload failed");
                }
            } catch(Exception e) {
                throw e;
            }


        }
    }
}