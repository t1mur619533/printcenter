using System.Linq;
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

namespace PrintCenter.Domain.Users
{
    public class Login
    {
        public class UserDto
        {
            public string Login { get; set; }

            public string Password { get; set; }
        }

        public class UserDataValidator : AbstractValidator<UserDto>
        {
            public UserDataValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
            }
        }

        public class Command : IRequest<UserEnvelope>
        {
            public UserDto UserDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserDto).NotNull().SetValidator(new UserDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, UserEnvelope>
        {
            private readonly IDataContext context;
            private readonly IMapper mapper;
            private readonly IPasswordHasher<Data.Models.User> passwordHasher;
            private readonly IJwtTokenGenerator jwtTokenGenerator;

            public Handler(IDataContext context, IPasswordHasher<Data.Models.User> passwordHasher, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator)
            {
                this.context = context;
                this.passwordHasher = passwordHasher;
                this.mapper = mapper;
                this.jwtTokenGenerator = jwtTokenGenerator;
            }

            public async Task<UserEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var user = await context.DbSet<Data.Models.User>().Where(x => x.Login == message.UserDto.Login)
                    .SingleOrDefaultAsync(cancellationToken);
                if (user == null)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, "Invalid login / password.");
                }

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, message.UserDto.Password) ==
                    PasswordVerificationResult.Failed)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, "Invalid login / password.");
                }

                var userDto = mapper.Map<User>(user);
                userDto.Token = jwtTokenGenerator.CreateToken(user.Login, user.Role.ToString());

                return new UserEnvelope {User = userDto};
            }
        }
    }
}