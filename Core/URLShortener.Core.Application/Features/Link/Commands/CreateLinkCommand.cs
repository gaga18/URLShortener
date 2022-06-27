using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Project.Core.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using URLShortener.Core.Domain.Entities;
using Workabroad.Core.Application.Exceptions;

namespace URLShortener.Core.Application.Features.Link.Commands
{
    public class CreateLinkCommand
    {
        public class Request : IRequest<string>
        {
            public string OriginUrl { get; set; }
            public string Note { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Handler : IRequestHandler<Request, string>
        {
            private readonly IUnitOfWork unit;
            private readonly IMapper mapper;
            private readonly IUrlHelper urlHelper;

            public Handler(IUnitOfWork unit, IMapper mapper, IUrlHelper urlHelper)
            {
                this.unit = unit;
                this.mapper = mapper;
                this.urlHelper = urlHelper;
            }
            public async Task<string> Handle(Request request, CancellationToken cancellationToken)
            {
                if(!Uri.IsWellFormedUriString(request.OriginUrl, UriKind.Absolute))
                    throw new EntityAlreadyExistsException("Invalid OriginUrl");

                if (urlHelper.IsLocalUrl(request.OriginUrl))
                    throw new EntityAlreadyExistsException("Url is Local");

                var dblink = await unit.LinkRepository.getByOriginUrl(request.OriginUrl);

                if (dblink != null)
                    throw new EntityAlreadyExistsException($"OriginUrl already exists for token '{dblink.FwToken}'");

                string token;
                do
                {
                    token = Guid.NewGuid().ToString()[..8].ToLower();
                } while (await unit.LinkRepository.CheckAsync(p => p.FwToken == token));


                var link = mapper.Map<LinkEntity>(request);

                link.FwToken = token;

                await unit.LinkRepository.Add(link);
                unit.Complete();

                return token;
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.OriginUrl)
                    .NotEmpty().WithMessage("OriginUrl required");
              
            }
        }
    }
}
