﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel_booking_core.Interface
{
    public interface IImageService
    {
        Task<UploadResult> UploadAsync(IFormFile image);
        Task<DelResResult> DeleteResourcesAsync(string publicId);
    }
}