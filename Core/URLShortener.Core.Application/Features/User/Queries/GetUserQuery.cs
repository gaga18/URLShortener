using AutoMapper;
using Project.Core.Application.DTOs;
using Project.Core.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Workabroad.Core.Application.Exceptions;

namespace Project.Core.Application.Features.Users.Queries
{
    public class GetUserQuery
    {
        public class Request : IRequest<GetUserDto>
        {
            public int UserId { get; private set; }

            public Request(int UserId) => this.UserId = UserId;
        }

        public class Handler : IRequestHandler<Request, GetUserDto>
        {
            private readonly IUnitOfWork unit;
            private readonly IMapper mapper;

            public Handler(IUnitOfWork unit, IMapper mapper)
            {
                this.unit = unit;
                this.mapper = mapper;
            }

            public async Task<GetUserDto> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await unit.UserRepository.Get(request.UserId);
                if (user == null)
                    throw new EntityNotFoundException("No User found");

                return await Task.FromResult(mapper.Map<GetUserDto>(user));
            }
        }
    }
}
