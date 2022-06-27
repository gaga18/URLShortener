using AutoMapper;
using FluentValidation;
using MediatR;
using Project.Core.Application.DTOs;
using Project.Core.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using URLShortener.Core.Application.DTOs;

namespace URLShortener.Core.Application.Features.Link.Queries
{
    public class GetLinksQuery
    {
        public class Request : IRequest<GetPaginationDto<GetLinkDto>>
        {
            public int pageIndex { get; set; }
            public int pageSize { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string OriginalUrl { get; set; }
        }

        public class Handler : IRequestHandler<Request, GetPaginationDto<GetLinkDto>>
        {
            private readonly IUnitOfWork unit;
            private readonly IMapper mapper;

            public Handler(IUnitOfWork unit, IMapper mapper)
            {
                this.unit = unit;
                this.mapper = mapper;
            }

            public async Task<GetPaginationDto<GetLinkDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                var links = await unit.LinkRepository.FilterAsync(request.pageIndex, request.pageSize, originalUrl: request.OriginalUrl);

                return mapper.Map<GetPaginationDto<GetLinkDto>>(links);
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.pageIndex).GreaterThanOrEqualTo(1).WithMessage("Enter the PageIndex");
                RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("Enter the PageSize");
            }
        }
    }
}
