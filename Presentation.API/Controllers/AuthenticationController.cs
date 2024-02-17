using Application.Cqrs.Commands;
using Core.Application.Requests.Authorize.Command;
using Core.Application.Requests.Authorize.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AuthenticationController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO Request)
        {
            var command = new LoginRequest();
            command.LoginDTO = Request;
            var response = await _commandDispatcher.SendAsync(command);
            return Ok(response);
        }
    }
}
