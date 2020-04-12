using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Infrastructure.Security;

namespace PrintCenter.Domain.Accounts
{
    public class Login
    {
        public class AccountDto
        {
            public string Login { get; set; }

            public string Password { get; set; }
        }

        public class AccountDtoValidator : AbstractValidator<AccountDto>
        {
            public AccountDtoValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<AccountEnvelope>
        {
            public AccountDto AccountDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AccountDto).NotNull().SetValidator(new AccountDtoValidator());
            }
        }

        public class Handler : IRequestHandler<Command, AccountEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;
            private readonly IPasswordHasher<Data.Models.User> passwordHasher;
            private readonly IJwtTokenGenerator jwtTokenGenerator;

            public Handler(DataContext context, IPasswordHasher<Data.Models.User> passwordHasher, IMapper mapper,
                IJwtTokenGenerator jwtTokenGenerator)
            {
                this.context = context;
                this.passwordHasher = passwordHasher;
                this.mapper = mapper;
                this.jwtTokenGenerator = jwtTokenGenerator;
            }

            public async Task<AccountEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var user = await context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Login == message.AccountDto.Login, cancellationToken);
                if (user == null)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, "Invalid login / password.");
                }

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, message.AccountDto.Password) ==
                    PasswordVerificationResult.Failed)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, "Invalid login / password.");
                }

                var account = mapper.Map<Account>(user);
                account.Token = jwtTokenGenerator.CreateToken(user.Login, user.Role.ToString());

                return new AccountEnvelope(account);
            }
        }
    }
}