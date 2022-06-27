using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Project.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UAParser;
using URLShortener.Core.Application.DTOs;

namespace URLShortener.Core.Application.Features.Report.Queries
{
    public class GetClientTypeCountsQuery
    {
        public class Request : IRequest<IReadOnlyList<GetClientTypeCountDto>>
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class Handler : IRequestHandler<Request, IReadOnlyList<GetClientTypeCountDto>>
        {
            private readonly IUnitOfWork unit;
            private readonly IConfiguration configuration;

            public Handler(IUnitOfWork unit, IConfiguration configuration)
            {
                this.unit = unit;
                this.configuration = configuration;;
            }

            public async Task<IReadOnlyList<GetClientTypeCountDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                var uaParser = Parser.GetDefault();
                var topTypes = configuration.GetSection("AppSettings:TopClientTypes").Get<int>();

                string GetClientTypeName(string userAgent)
                {
                    if (string.IsNullOrWhiteSpace(userAgent)) return "N/A";

                    var c = uaParser.Parse(userAgent);
                    return $"{c.OS.Family}-{c.UA.Family}";
                }

                var LinkTracking = await unit.LinkTrackingRepository.GetAll();
                var uac = LinkTracking.Where(p =>
                            p.RequestTime.Date <= request.EndDate.Date &&
                            p.RequestTime.Date >= request.StartDate.Date)
                        .GroupBy(p => p.UserAgent)
                        .Select(p => new UserAgentCount
                        {
                            RequestCount = p.Count(),
                            UserAgent = p.Key
                        });

                if (uac.Any())
                {
                    var q = from d in uac
                            group d by GetClientTypeName(d.UserAgent)
                            into g
                            select new GetClientTypeCountDto
                            {
                                ClientTypeName = g.Key,
                                Count = g.Sum(gp => gp.RequestCount)
                            };

                    if (topTypes > 0) q = q.OrderByDescending(p => p.Count).Take(topTypes);
                    return q.ToList();
                }
                return new List<GetClientTypeCountDto>();
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.StartDate)
                    .NotEmpty().WithMessage("StartDate required");
                RuleFor(x => x.EndDate)
                    .NotEmpty().WithMessage("EndDate required");
            }
        }

    }
}
