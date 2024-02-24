using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Uploader;
using Services.Services.Uploader.DTO;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        private readonly IUploaderService _uploaderService;

        public UploaderController(IUploaderService uploaderService)
        {
            _uploaderService = uploaderService;
        }

        /// <summary>
        /// this is for test upload webp
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadAsWebp([FromForm]UploadDTO Request)
        {
            return Ok(await _uploaderService.UploadAsWebp(Request));
        }

        

        /// <summary>
        /// this is for test upload jepg
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadAsJepg([FromForm] UploadDTO Request)
        {
            return Ok(await _uploaderService.UploadAsJpeg(Request));
        }

        /// <summary>
        /// this is for test upload jpg
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadAsJpg([FromForm] UploadDTO Request)
        {
            return Ok(await _uploaderService.UploadAsJpg(Request));
        }

        /// <summary>
        /// this is for test upload png
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadAsPng([FromForm] UploadDTO Request)
        {
            return Ok(await _uploaderService.UploadAsPng(Request));
        }
    }
}
