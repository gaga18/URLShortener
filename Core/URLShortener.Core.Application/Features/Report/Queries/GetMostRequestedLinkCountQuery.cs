using FluentValidation;
using MediatR;
using Project.Core.Application.DTOs;
using Project.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Core.Application.Features.Report.Queries
{
    public class GetMostRequestedLinkCountQuery
    {
        public class Request : IRequest<IReadOnlyList<GetMostRequestedLinkCountDto>>
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class Handler : IRequestHandler<Request, IReadOnlyList<GetMostRequestedLinkCountDto>>
        {
            private readonly IUnitOfWork unit;

            public Handler(IUnitOfWork unit)
            {
                this.unit = unit;
            }

            public async Task<IReadOnlyList<GetMostRequestedLinkCountDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                var LinkTracking = await unit.LinkTrackingRepository.GetAllWithLinks();

                var report = LinkTracking
                            .Where(p => p.RequestTime.Date <= request.EndDate.Date &&
                                        p.RequestTime.Date >= request.StartDate.Date)
                            .GroupBy(lt => new { lt.Link.FwToken, lt.Link.Note })
                            .Select(g => new GetMostRequestedLinkCountDto
                            {
                                Note = g.Key.Note,
                                FwToken = g.Key.FwToken,
                                RequestCount = g.Count()
                            });

                return report.ToList();
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
