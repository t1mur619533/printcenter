using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Streams
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Code { get; set; }

            public Command(string code)
            {
                Code = code;
            }
        }

        public class QueryHandler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            public QueryHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var stream =
                    await context.Streams.FirstOrDefaultAsync(x => x.Code.Equals(command.Code), cancellationToken);
                if (stream == null)
                {
                    throw new NotFoundException<Shared.Stream>($"id {command.Code}");
                }

                context.Streams.Remove(stream);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}