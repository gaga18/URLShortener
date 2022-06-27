using MediatR;
using Project.Core.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Workabroad.Core.Application.Exceptions;

namespace Project.Core.Application.Features.Link.Commands
{
    public class DeleteLinkCommand
    {
        public class Request : IRequest
        {
            public int LinkId { get; private set; }

            public Request(int LinkId) => this.LinkId = LinkId;
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IUnitOfWork unit;

            public Handler(IUnitOfWork unit) =>
                this.unit = unit;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var isRecord = await unit.LinkRepository.CheckAsync(x => x.Id == request.LinkId);
                if (!isRecord)
                    throw new EntityNotFoundException("Record not found");

                await unit.LinkRepository.Delete(request.LinkId);
                unit.Complete();

                return Unit.Value;
            }
        }
    }
}
