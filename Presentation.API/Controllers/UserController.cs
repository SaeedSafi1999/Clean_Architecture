using Application.Cqrs.Commands;
using Application.Cqrs.Queris;
using Core.Application.Extensions.Mapper;
using Core.Application.Mediator.User.Query;
using Core.Application.Requests.User.Command;
using Core.Application.Requests.User.DTO;
using Core.Application.Requests.User.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
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


        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO commnad)
        {
            var query = new CreateUserCommand();
            query.CreateUserDTO = commnad;
            var response = await _commandDispatcher.SendAsync(query);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var respone = await _queryDispatcher.SendAsync(query);
            return Ok(respone);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var query = new GetUserInfoQuery(User.GetUserId());
            var respone = await _queryDispatcher.SendAsync(query);
            return Ok(respone);
        }
    }
}
