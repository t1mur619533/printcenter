using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PrintCenter.Data;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Accounts
{
    public class EditPassword
    {
        public class Command : IRequest
        {
            [JsonIgnore]
            public string Login { get; set; }

            public string OldPassword { get; set; }

            public string NewPassword { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.OldPassword).NotNull().NotEmpty().Length(6, 255);
                RuleFor(x => x.NewPassword).NotNull().NotEmpty().Length(6, 255);
                RuleFor(x => x.Login).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IPasswordHasher<User> passwordHasher;

            public Handler(DataContext context, IPasswordHasher<User> passwordHasher)
            {
                this.context = context;
                this.passwordHasher = passwordHasher;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Login.Equals(command.Login), cancellationToken);

                if (user == null)
                {
                    throw new InvalidArgumentException("Invalid login / password.");
                }

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.OldPassword)
                    .Equals(PasswordVerificationResult.Failed))
                {
                    throw new InvalidArgumentException("Invalid login / password.");
                }

                user.PasswordHash = passwordHasher.HashPassword(user, command.NewPassword);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}