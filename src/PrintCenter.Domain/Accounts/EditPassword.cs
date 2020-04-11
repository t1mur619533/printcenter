using System.Linq;
using System.Net;
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
        public class AccountDto
        {
            public string OldPassword { get; set; }

            public string NewPassword { get; set; }
        }

        public class Command : IRequest
        {
            [JsonIgnore]
            public string Login { get; set; }

            public AccountDto UserDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserDto).NotNull();
                RuleFor(x => x.UserDto.OldPassword).NotNull().NotEmpty();
                RuleFor(x => x.UserDto.NewPassword).NotNull().NotEmpty();
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
                var user = await context.Users
                    .Where(x => x.Login.Equals(command.Login))
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest);
                }

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.UserDto.OldPassword)
                    .Equals(PasswordVerificationResult.Failed))
                {
                    throw new RestException(HttpStatusCode.Unauthorized, "Invalid login / password.");
                }

                user.PasswordHash = passwordHasher.HashPassword(user, command.UserDto.NewPassword);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}