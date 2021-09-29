﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using hotel_booking_core.CloudinaryService.Interface;
using hotel_booking_models.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotel_booking_core.CloudinaryService.Service
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary cloudinary;
        private readonly ImageUploadSettings _accountSettings;
        public ImageService(IConfiguration config, IOptions<ImageUploadSettings> accountSettings )
        {
            _accountSettings = accountSettings.Value;
            _config = config;
            cloudinary = new Cloudinary(new Account(_accountSettings.CloudName,
                _accountSettings.ApiKey, _accountSettings.ApiSecret));
        }
        public async Task<UploadResult> UploadAsync(IFormFile image)
        {
            var pictureSize = Convert.ToInt64(_config.GetSection("PhotoSettings:Size").Get<string>());
            if (image.Length > pictureSize)
            {
                throw new ArgumentException("File size exceeded");
            }
            var pictureFormat = false;

            var listOfImageExtensions = _config.GetSection("PhotoSettings:Formats").Get<List<string>>();

            foreach (var item in listOfImageExtensions)
            {
                if (image.FileName.EndsWith(item))
                {
                    pictureFormat = true;
                    break;
                }
            }

            if (pictureFormat == false)
            {
                throw new ArgumentException("File format not supported");
            }

            var uploadResult = new ImageUploadResult();
           
            using (var imageStream = image.OpenReadStream())
            {
                string filename = Guid.NewGuid().ToString() + image.FileName;
                 
                uploadResult = await cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(filename + Guid.NewGuid().ToString(), imageStream),
                    PublicId = "Hotel Listings/" + filename,
                    
                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(150)
                });
            }
            var s = uploadResult.PublicId;

            return uploadResult;
        }

        public async Task<DelResResult> DeleteResourcesAsync(string publicId) 
        {
            DelResParams delParams = new DelResParams
            {
                PublicIds = new List<string> { publicId },
                All = true,
                KeepOriginal = false,
                Invalidate = true
            };
            

            DelResResult deletionResult =await cloudinary.DeleteResourcesAsync(delParams);
            if(deletionResult.Error != null )
            {
                throw new ApplicationException($"" +
                    $"an error occured in method: " +
                    $"{nameof(DeleteResourcesAsync)}" +
                    $" class: {nameof(ImageService)}");
            }

            return deletionResult;
        }

        
    }
}