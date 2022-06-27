using Project.Core.Application.DTOs;
using Project.Core.Application.Features.Users.Commands;
using Project.Core.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortener.Core.Application.Features.User.Commands;

namespace Project.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator) =>
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<string> Authenticate([FromBody] LoginCommand.Request request) =>
            await mediator.Send(request);

        [HttpGet]
        public async Task<IEnumerable<GetUserDto>> Get([FromQuery] GetUsersQuery.Request request)
        {
            var result = await mediator.Send(request);

            Response.Headers.Add("PageIndex", result.PageIndex.ToString());
            Response.Headers.Add("PageSize", result.PageSize.ToString());

            Response.Headers.Add("TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("TotalCount", result.TotalCount.ToString());

            Response.Headers.Add("HasPreviousPage", result.HasPreviousPage.ToString());
            Response.Headers.Add("HasNextPage", result.HasNextPage.ToString());

            return result.Items;
        }

        [HttpGet("{id}")]
        public async Task<GetUserDto> Get([FromRoute] int id) =>
            await mediator.Send(new GetUserQuery.Request(id));

        [HttpPost]
        public async Task Post([FromBody] CreateUserCommand.Request request) =>
            await mediator.Send(request);

        [HttpPut]
        public async Task Put([FromBody] UpdateUserCommand.Request request)
        {
            await mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id) =>
            await mediator.Send(new DeleteUserCommand.Request(id));
    }
}
