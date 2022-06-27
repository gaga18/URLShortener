using Project.Core.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Workabroad.Core.Application.Exceptions;

namespace Project.Core.Application.Features.Users.Commands
{
    public class DeleteUserCommand
    {
        public class Request : IRequest
        {
            public int UserId { get; private set; }

            public Request(int UserId) => this.UserId = UserId;
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IUnitOfWork unit;

            public Handler(IUnitOfWork unit) =>
                this.unit = unit;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var isRecord = await unit.UserRepository.CheckAsync(x => x.Id == request.UserId);
                if (!isRecord)
                    throw new EntityNotFoundException("User not found");

                await unit.UserRepository.Delete(request.UserId);
                unit.Complete();

                return Unit.Value;
            }
        }
    }
}
