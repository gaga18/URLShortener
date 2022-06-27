using MediatR;
using Microsoft.Extensions.Primitives;
using Project.Core.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using URLShortener.Core.Domain.Entities;
using Workabroad.Core.Application.Exceptions;

namespace URLShortener.Core.Application.Features.Link.Queries
{
    public class ForwardQuery
    {
        public class Request : IRequest<string>
        {
            public string Ip { get; private set; }
            public string UserAgent { get; private set; }
            public string Token { get; private set; }
            public Request(string ip, StringValues userAgent, string token)
            {
                this.Ip = ip;
                this.UserAgent = userAgent;
                this.Token = token;
            }
        }

        public class Handler : IRequestHandler<Request, string>
        {
            private readonly IUnitOfWork unit;

            public Handler(IUnitOfWork unit) =>
                this.unit = unit;

            public async Task<string> Handle(Request request, CancellationToken cancellationToken)
            {
                var link = await unit.LinkRepository.getByToken(request.Token);

                if (link == null)
                    throw new EntityNotFoundException("Invalid token");

                var linkTracking = new LinkTracking()
                {
                    IpAddress = request.Ip,
                    LinkId = link.Id,
                    RequestTime = DateTime.Now,
                    UserAgent = request.UserAgent
                };

                await unit.LinkTrackingRepository.Add(linkTracking);
                unit.Complete();

                return link.OriginUrl;
            }
        }
    }
}
