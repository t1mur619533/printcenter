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
using PrintCenter.Shared;
using User = PrintCenter.Data.Models.User;

namespace PrintCenter.Domain.Accounts
{
    public class Login
    {
        public class Command : IRequest<Account>
        {
            public LoginData LoginData { get; set; }

            public Command(LoginData loginData)
            {
                LoginData = loginData;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.LoginData.Login).NotNull().NotEmpty();
                RuleFor(x => x.LoginData.Password).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Account>
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

            public async Task<Account> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await context.Users.AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Login.Equals(command.LoginData.Login), cancellationToken);
                if (user == null)
                {
                    throw new InvalidArgumentException("Invalid login / password.");
                }

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.LoginData.Password) ==
                    PasswordVerificationResult.Failed)
                {
                    throw new InvalidArgumentException("Invalid login / password.");
                }

                if (user.Role == Role.Disable)
                {
                    throw new AccessDeniedException("Account is blocked.");
                }

                if (user.Role == Role.Disable)
                {
                    throw new AccessDeniedException("Account is blocked.");
                }

                var account = mapper.Map<Account>(user);
                account.Token = jwtTokenGenerator.CreateToken(user.Login, user.Role.ToString());
                return account;
            }
        }
    }
}