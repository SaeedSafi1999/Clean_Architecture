using Application.Cqrs.Commands;
using Core.Application.Requests.User.Command;
using Core.Application.Requests.User.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UserController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserDTO commnad)
        {
            var query = new CreateUserCommand();
            query.CreateUserDTO = commnad;
            var response = await _commandDispatcher.SendAsync(query);
            return Ok(response);
        }
    }
}
