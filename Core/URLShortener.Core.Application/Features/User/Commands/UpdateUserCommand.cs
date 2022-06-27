using AutoMapper;
using FluentValidation;
using MediatR;
using Project.Core.Application.Interfaces;
using Project.Core.Domain.Entities;
using Project.Core.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using URLShortener.Core.Application.Extension;
using Workabroad.Core.Application.Exceptions;

namespace Project.Core.Application.Features.Users.Commands
{
    public class UpdateUserCommand
    {
        public class Request : IRequest
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public string UserName { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public DateTime BirthdayDate { get; set; }
            public int? RoleId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IUnitOfWork unit;
            private readonly IMapper mapper;

            public Handler(IUnitOfWork unit, IMapper mapper)
            {
                this.unit = unit;
                this.mapper = mapper;
            }
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await unit.UserRepository.Get(request.Id);

                if (user == null)
                    throw new EntityNotFoundException("User not found");

                if (!string.IsNullOrEmpty(request.NewPassword))
                {
                    if(request.OldPassword.ToSHA512() == user.Password)
                    {
                        user.Password = request.NewPassword.ToSHA512();
                    }
                    else
                    {
                        throw new EntityAlreadyExistsException("Incorect OldPassword");
                    }
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Gender = request.Gender;
                user.UserName = request.UserName;
                user.BirthdayDate = request.BirthdayDate;

                unit.UserRepository.Update(user);

                if(request.RoleId != null)
                {
                    var checkRole = await unit.RoleRepository.CheckAsync(x => x.Id == request.RoleId);
                    if (!checkRole)
                        throw new EntityNotFoundException("Wrong RoleId");

                    await unit.UserRoleRepository.Delete(user.UserRoles.FirstOrDefault().Id);

                    var userRole = new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = request.RoleId.Value
                    };
                    await unit.UserRoleRepository.Add(userRole);
                }

                unit.Complete();

                return Unit.Value;
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("FirstName required")
                    .MinimumLength(2).WithMessage("FirstName must consist of at least 2 characters")
                    .MaximumLength(50).WithMessage("FirstName must consist of a maximum of 50 characters");

                RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("LastName required")
                    .MinimumLength(2).WithMessage("LastName must consist of at least 2 characters")
                    .MaximumLength(50).WithMessage("LastName must consist of a maximum of 50 characters");

                When(x => !string.IsNullOrEmpty(x.OldPassword), () =>
                {
                    RuleFor(x => x.NewPassword)
                   .NotEmpty().WithMessage("Password is empty")
                   .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").WithMessage("Password must consist of at least one upper case English letter, one lower case English letter, one digit, one special character and Minimum eight in length");
                });


                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("UserName is empty")
                    .MinimumLength(3).WithMessage("Username must consist of at least 3 characters or numbers");
            }

        }
    }
}
