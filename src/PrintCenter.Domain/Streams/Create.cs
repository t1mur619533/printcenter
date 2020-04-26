using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Streams
{
    public class Create
    {
        public class Command : IRequest<Unit>
        {
            public Shared.Stream Stream { get; set; }

            public Command(Shared.Stream stream)
            {
                Stream = stream;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Stream).NotNull().NotEmpty();
                RuleFor(x => x.Stream.Code).NotNull().NotEmpty();
                RuleFor(x => x.Stream.Name).NotNull().NotEmpty();
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
                if (await context.Streams.Where(x => x.Code.Equals(command.Stream.Code)).AnyAsync(cancellationToken))
                {
                    throw new DuplicateException<Shared.Stream>(command.Stream.Name);
                }

                var stream = mapper.Map<Data.Models.Stream>(command.Stream);
                await context.Streams.AddAsync(stream, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}