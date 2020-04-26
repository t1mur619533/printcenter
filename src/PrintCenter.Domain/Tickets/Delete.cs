using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Tickets
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Command(string login, IEnumerable<int> notifications)
            {
                Tickets = notifications;
                Login = login;
            }

            public string Login { get; }

            public IEnumerable<int> Tickets { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty();
                RuleFor(x => x.Tickets).NotNull().Must(_ => _.Any());
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
                var notifications = await context.Tickets
                    .Include(_ => _.User)
                    .Where(_ => command.Tickets.Contains(_.Id) && _.User.Login.Equals(command.Login))
                    .ToListAsync(cancellationToken);

                if (notifications == null || !notifications.Any())
                {
                    throw new NotFoundException("Tickets not found.");
                }

                context.Tickets.RemoveRange(notifications);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}