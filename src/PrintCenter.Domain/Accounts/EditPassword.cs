using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Accounts
{
    public class EditPassword
    {
        public class Command : IRequest
        {
            public string Login { get; set; }

            public EditPasswordData EditPasswordData { get; set; }

            public Command(string login, EditPasswordData editPasswordData)
            {
                Login = login;
                EditPasswordData = editPasswordData;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.EditPasswordData.OldPassword).NotNull().NotEmpty().Length(6, 255);
                RuleFor(x => x.EditPasswordData.NewPassword).NotNull().NotEmpty().Length(6, 255);
                RuleFor(x => x.Login).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IPasswordHasher<Data.Models.User> passwordHasher;

            public Handler(DataContext context, IPasswordHasher<Data.Models.User> passwordHasher)
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

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.EditPasswordData.OldPassword)
                    .Equals(PasswordVerificationResult.Failed))
                {
                    throw new InvalidArgumentException("Invalid login / password.");
                }

                user.PasswordHash = passwordHasher.HashPassword(user, command.EditPasswordData.NewPassword);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}