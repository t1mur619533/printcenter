using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Technologies
{
    public class Edit
    {
        public class TechnologyDto
        {
            public string Name { get; set; }
        
            public string Unit { get; set; }

            public string Description { get; set; }
        }
        
        public class TechnologyDtoValidator : AbstractValidator<TechnologyDto>
        {
            public TechnologyDtoValidator()
            {
                RuleFor(tech => tech.Name).NotNull().NotEmpty();
                RuleFor(tech => tech.Unit).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<Unit>
        {
            public TechnologyDto TechnologyDto { get; set; }
            public int Id { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.TechnologyDto).NotNull().SetValidator(new TechnologyDtoValidator());
            }
        }
        
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var technology = await context.Technologies.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (technology == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                
                mapper.Map(command.TechnologyDto, technology);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}