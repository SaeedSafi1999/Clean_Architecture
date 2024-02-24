using Microsoft.AspNetCore.Http;
using Services.Services.Uploader.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Uploader
{
    public interface IUploaderService
    {
        Task<string> UploadAsWebp(UploadDTO Request);
        Task<string> UploadAsPng(UploadDTO Request);
        Task<string> UploadAsJpeg(UploadDTO Request);
        Task<string> UploadAsJpg(UploadDTO Request);
    }
}
