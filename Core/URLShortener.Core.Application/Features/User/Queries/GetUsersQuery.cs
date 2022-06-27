using AutoMapper;
using Project.Core.Application.DTOs;
using Project.Core.Application.Interfaces;

using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Core.Application.Features.Users.Queries
{
    public class GetUsersQuery
    {
        public class Request : IRequest<GetPaginationDto<GetUserDto>>
        {
            public int pageIndex { get; set; }
            public int pageSize { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Request, GetPaginationDto<GetUserDto>>
        {
            private readonly IUnitOfWork unit;
            private readonly IMapper mapper;

            public Handler(IUnitOfWork unit, IMapper mapper)
            {
                this.unit = unit;
                this.mapper = mapper;
            }

            public async Task<GetPaginationDto<GetUserDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                var Users = await unit.UserRepository.FilterAsync(request.pageIndex, request.pageSize, FirstName: request.FirstName, LastName: request.LastName, Username: request.UserName);

                return mapper.Map<GetPaginationDto<GetUserDto>>(Users);
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
