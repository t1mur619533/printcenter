using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Tickets
{
    public class Remind
    {
        public class Command : IRequest
        {
            public Command(string login, int id, int? minutes)
            {
                Id = id;
                Minutes = minutes;
                Login = login;
            }

            public string Login { get; set; }

            public int Id { get; set; }

            public int? Minutes { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty();
                RuleFor(x => x.Minutes).LessThanOrEqualTo(24 * 60 * 3)
                    .GreaterThanOrEqualTo(0); //можно отложить не более чем на 3 суток
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
                var notification = await context.Tickets
                    .Include(_ => _.User)
                    .FirstOrDefaultAsync(x => x.Id.Equals(command.Id) && x.User.Login.Equals(command.Login),
                        cancellationToken);

                if (notification == null)
                {
                    throw new NotFoundException("Notifications not found.");
                }

                notification.DelayedDate = DateTime.Now.AddMinutes(command.Minutes ?? 0);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}