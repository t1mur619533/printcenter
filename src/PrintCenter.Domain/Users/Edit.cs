using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Exceptions;
using User = PrintCenter.Shared.User;

namespace PrintCenter.Domain.Users
{
    public class Edit
    {
        public class Command : IRequest
        {
            public User User { get; set; }

            public Command(User user)
            {
                User = user;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).NotNull();
                RuleFor(x => x.User.Name).NotNull().NotEmpty().Length(1,255);
                RuleFor(x => x.User.Surname).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.User.Role).Must(s => Enum.TryParse<Role>(s, out _)).WithMessage("Invalid role");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IPasswordHasher<Data.Models.User> hasher;

            public Handler(DataContext context, IPasswordHasher<Data.Models.User> hasher)
            {
                this.context = context;
                this.hasher = hasher;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await context.Users
                    .Where(x => x.Login.Equals(command.User.Login))
                    .Include(u => u.UserTechnologies)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException<User>(command.User.Login);
                }

                user.Name = command.User.Name ?? user.Name;
                user.Surname = command.User.Surname ?? user.Surname;
                user.Role = Enum.Parse<Role>(command.User.Role);

                if (!string.IsNullOrWhiteSpace(command.User.Password))
                {
                    user.PasswordHash = hasher.HashPassword(user, command.User.Password);
                }

                user.UserTechnologies.RemoveAll(technology => technology.UserId.Equals(user.Id));

                if (command.User.TechnologyNames != null)
                    foreach (var technologyName in command.User.TechnologyNames)
                    {
                        var technology =
                            await context.Technologies.SingleOrDefaultAsync(_ => _.Name.Equals(technologyName),
                                cancellationToken);
                        if (technology != null)
                            user.UserTechnologies.Add(new UserTechnology
                                {UserId = user.Id, TechnologyId = technology.Id});
                    }

                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}