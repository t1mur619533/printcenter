using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Technologies
{
    public class Edit
    {
        public class Command : IRequest<Unit>
        {
            public Technology Technology { get; set; }

            public Command(Technology technology)
            {
                Technology = technology;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Technology).NotNull();
                RuleFor(x => x.Technology.Name).NotNull().NotEmpty();
                RuleFor(x => x.Technology.Unit).NotNull().NotEmpty();
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
                var technology =
                    await context.Technologies.FirstOrDefaultAsync(x => x.Id == command.Technology.Id, cancellationToken);

                if (technology == null)
                {
                    throw new NotFoundException<Technology>($"id {command.Technology.Name}");
                }

                mapper.Map(command.Technology, technology);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}