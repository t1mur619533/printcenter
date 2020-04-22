using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Infrastructure.Security;

namespace PrintCenter.Domain.Accounts
{
    public class Login
    {
        public class Command : IRequest<AccountEnvelope>
        {
            public string Login { get; set; }

            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, AccountEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;
            private readonly IPasswordHasher<User> passwordHasher;
            private readonly IJwtTokenGenerator jwtTokenGenerator;

            public Handler(DataContext context, IPasswordHasher<User> passwordHasher, IMapper mapper,
                IJwtTokenGenerator jwtTokenGenerator)
            {
                this.context = context;
                this.passwordHasher = passwordHasher;
                this.mapper = mapper;
                this.jwtTokenGenerator = jwtTokenGenerator;
            }

            public async Task<AccountEnvelope> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await context.Users.AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Login == command.Login, cancellationToken);
                if (user == null)
                {
                    throw new InvalidArgumentException("Invalid login / password.");
                }

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password) ==
                    PasswordVerificationResult.Failed)
                {
                    throw new InvalidArgumentException("Invalid login / password.");
                }

                if (user.Role == Role.Disable)
                {
                    throw new AccessDeniedException("Account is blocked.");
                }

                var account = mapper.Map<Account>(user);
                account.Token = jwtTokenGenerator.CreateToken(user.Login, user.Role.ToString());

                return new AccountEnvelope(account);
            }
        }
    }
}