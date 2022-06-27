using AutoMapper;
using MediatR;
using Project.Core.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using URLShortener.Core.Application.DTOs;
using Workabroad.Core.Application.Exceptions;

namespace URLShortener.Core.Application.Features.Link.Queries
{
    public class GetLinkQuery
    {

        public class Request : IRequest<GetLinkDto>
        {
            public int LinkId { get; private set; }

            public Request(int LinkId) => this.LinkId = LinkId;
        }

        public class Handler : IRequestHandler<Request, GetLinkDto>
        {
            private readonly IUnitOfWork unit;
            private readonly IMapper mapper;

            public Handler(IUnitOfWork unit, IMapper mapper)
            {
                this.unit = unit;
                this.mapper = mapper;
            }

            public async Task<GetLinkDto> Handle(Request request, CancellationToken cancellationToken)
            {
                var link = await unit.LinkRepository.Get(request.LinkId);
                if (link == null)
                    throw new EntityNotFoundException("No Link found");

                return await Task.FromResult(mapper.Map<GetLinkDto>(link));
            }
        }
    }
}
