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
using PrintCenter.Data.Models;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Users
{
    public class Create
    {
        public class UserDto
        {
            public string Login { get; set; }

            public string Password { get; set; }

            public string Surname { get; set; }

            public string Name { get; set; }

            public Role Role { get; set; }
        }

        public class UserDtoValidator : AbstractValidator<UserDto>
        {
            public UserDtoValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Password).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Role).IsInEnum();
                RuleFor(x => x.Surname).NotNull().NotEmpty().Length(1, 255);
            }
        }

        public class Command : IRequest
        {
            public UserDto UserDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserDto).NotNull().SetValidator(new UserDtoValidator());
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDataContext context;
            private readonly IPasswordHasher<Data.Models.User> hasher;
            private readonly IMapper mapper;

            public Handler(IDataContext context, IMapper mapper, IPasswordHasher<Data.Models.User> hasher)
            {
                this.context = context;
                this.mapper = mapper;
                this.hasher = hasher;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                if (await context.DbSet<Data.Models.User>().Where(x => x.Login == command.UserDto.Login)
                    .AnyAsync(cancellationToken))
                {
                    throw new RestException(HttpStatusCode.BadRequest,$"User with login {command.UserDto.Login} already exits.");
                }

                var user = mapper.Map<Data.Models.User>(command.UserDto);
                user.PasswordHash = hasher.HashPassword(user, command.UserDto.Password);

                await context.DbSet<Data.Models.User>().AddAsync(user, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}