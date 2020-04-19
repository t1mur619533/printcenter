using System.Linq;
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
    public class Create
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
                if (await context.Technologies.Where(x => x.Name == command.TechnologyDto.Name).AnyAsync(cancellationToken))
                {
                    throw new DuplicateException<Technology>(command.TechnologyDto.Name);
                }

                var technology = mapper.Map<Data.Models.Technology>(command.TechnologyDto);

                await context.Technologies.AddAsync(technology, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}