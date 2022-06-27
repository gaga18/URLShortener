using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Application.DTOs;
using Project.Core.Application.Features.Report.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortener.Core.Application.DTOs;
using URLShortener.Core.Application.Features.Report.Queries;

namespace Project.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,User")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;
        public ReportController(IMediator mediator) =>
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


        [HttpPost("requests/link")]
        public async Task<IReadOnlyList<GetMostRequestedLinkCountDto>> MostRequestedLinks([FromForm] GetMostRequestedLinkCountQuery.Request request) =>
            await mediator.Send(request);

        [HttpPost("requests/clienttype")]
        public async Task<IReadOnlyList<GetClientTypeCountDto>> ClientType([FromForm] GetClientTypeCountsQuery.Request request) =>
            await mediator.Send(request);

    }
}
