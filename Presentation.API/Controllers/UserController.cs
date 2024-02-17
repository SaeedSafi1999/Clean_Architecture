using Application.Cqrs.Commands;
using Application.Cqrs.Queris;
using Core.Application.Requests.User.Command;
using Core.Application.Requests.User.DTO;
using Core.Application.Requests.User.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserDTO commnad)
        {
            var query = new CreateUserCommand();
            query.CreateUserDTO = commnad;
            var response = await _commandDispatcher.SendAsync(query);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var respone = await _queryDispatcher.SendAsync(query);
            return Ok(respone);
        }
    }
}
