using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Streams
{
    public class Edit
    {
        public class Command : IRequest<Unit>
        {
            public Stream Stream { get; set; }

            public Command(Stream stream)
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
                var stream =
                    await context.Streams.FirstOrDefaultAsync(x => x.Code.Equals(command.Stream.Code),
                        cancellationToken);

                if (stream == null)
                {
                    throw new NotFoundException<Stream>($"id {command.Stream.Code}");
                }

                mapper.Map(command.Stream, stream);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}