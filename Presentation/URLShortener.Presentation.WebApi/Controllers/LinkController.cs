using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Project.Core.Application.Features.Link.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using URLShortener.Core.Application.DTOs;
using URLShortener.Core.Application.Features.Link.Commands;
using URLShortener.Core.Application.Features.Link.Queries;

namespace URLShortener.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,User")]
    public class LinkController : ControllerBase
    {
        private readonly IMediator mediator;
        private StringValues UserAgent => Request.Headers["User-Agent"];
        public LinkController(IMediator mediator) =>
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet]
        public async Task<IEnumerable<GetLinkDto>> Get([FromQuery] GetLinksQuery.Request request)
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
        public async Task<GetLinkDto> Get([FromRoute] int id) =>
            await mediator.Send(new GetLinkQuery.Request(id));

        [HttpPost]
        public async Task<string> Post([FromBody] CreateLinkCommand.Request request) =>
            await mediator.Send(request);

        [HttpPut]
        public async Task Put([FromBody] UpdateLinkCommand.Request request)
        {
            await mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id) =>
            await mediator.Send(new DeleteLinkCommand.Request(id));

        [HttpGet("/fw/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> Forward([FromRoute] string token)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "N/A";

            var url = await mediator.Send(new ForwardQuery.Request(ip, UserAgent, token));
            return Redirect(url);
        }
    }
}
