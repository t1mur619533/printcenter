using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Users
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Command(string login)
            {
                Login = login;
            }

            public string Login { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty();
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
                var user = await context.Users.FirstOrDefaultAsync(x => x.Login.Equals(command.Login), cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException<User>(command.Login);
                }

                context.Users.Remove(user);

                await context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}