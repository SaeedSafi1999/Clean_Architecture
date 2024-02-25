using Core.Application.Services.EncryptServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EncryptController : ControllerBase
    {
        private readonly IEncryptService _encryptService;

        public EncryptController(IEncryptService encryptService)
        {
            _encryptService = encryptService;
        }

        [HttpGet]
        public IActionResult Encrypt(string text)
        {
            return Ok(_encryptService.Encrypt(text));
        }

        [HttpGet]
        public IActionResult Decrypt(string text)
        {
            return Ok(_encryptService.Decrypt(text));
        }
    }
}
