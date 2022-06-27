using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using URLShortener.Core.Application.Exceptions;
using URLShortener.Core.Application.Extension;

namespace URLShortener.Core.Application.Features.User.Commands
{
    public class LoginCommand
    {
        public class Request : IRequest<string>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Request, string>
        {
            private readonly IUnitOfWork unit;
            private readonly IConfiguration config;

            public Handler(IUnitOfWork unit, IConfiguration config)
            {
                this.unit = unit;
                this.config = config;
            }
            public async Task<string> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await unit.UserRepository.GetUserByUserName(request.UserName);

                if (user == null || user.Password != request.Password.ToSHA512())
                    throw new ActionForbiddenException("Username or Password is incorrect");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var roles = await unit.UserRoleRepository.GetUserRolesAsync(user.Id);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]));
                var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var jwt = new JwtSecurityToken
                    (
                        claims: claims,
                        expires: DateTime.UtcNow.AddHours(1),
                        issuer: config["Token:Issuer"],
                        audience: config["Token:Audience"],
                        signingCredentials: signinCredentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("UserName is empty")
                    .MinimumLength(3).WithMessage("Username must consist of at least 3 characters or numbers");

            }
        }
    }
}
