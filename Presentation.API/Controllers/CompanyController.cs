using Core.Application.Requests.Company.Commands;
using Core.Application.Requests.Company.Queries;
using Core.Domain.DTOs.Company;
using Core.Domain.DTOs.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebFramework.Api;

namespace Presentation.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCompanies")]
        public async Task<ActionResult<List<CompanyDTO>>> GetAll(CancellationToken cancellationToken = default)
        {
            GetAllComapniesQuery Query = new();
            List<CompanyDTO> Response = await _mediator.Send(Query, cancellationToken);
            return Ok(Response);
        }

        [HttpPost("AddCompany")]
        public async Task<ActionResult<ServiceRespnse<bool>>> AddCompany(AddCompanyDTO Request, CancellationToken cancellationToken = default)
        {
            AddCompanyCommand Command = new();
            Command.addCompanyDTO = Request;

            ServiceRespnse<bool> Response = await _mediator.Send(Command, cancellationToken);

            return Ok(Response);
        }
    }
}
