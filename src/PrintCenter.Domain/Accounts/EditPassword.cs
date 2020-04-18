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

            public AccountDto AccountDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AccountDto).NotNull();
                RuleFor(x => x.AccountDto.OldPassword).NotNull().NotEmpty().Length(6, 255);
                RuleFor(x => x.AccountDto.NewPassword).NotNull().NotEmpty().Length(6, 255);
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
                    throw new RestException(HttpStatusCode.BadRequest);
                }

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.AccountDto.OldPassword)
                    .Equals(PasswordVerificationResult.Failed))
                {
                    throw new RestException(HttpStatusCode.Unauthorized, "Invalid login / password.");
                }

                user.PasswordHash = passwordHasher.HashPassword(user, command.AccountDto.NewPassword);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}