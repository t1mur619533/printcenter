using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Data.Models;

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
                RuleFor(x => x.Login).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
                RuleFor(x => x.Name).NotNull().NotEmpty();
                RuleFor(x => x.Surname).NotNull().NotEmpty();
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
                RuleFor(x => x.UserDto).NotNull().SetValidator(new UserDtoValidator());
            }
        }

        public class Handler : IRequestHandler<Command, UserEnvelope>
        {
            private readonly IDataContext context;
            private readonly IMapper mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<UserEnvelope> Handle(Command command, CancellationToken cancellationToken)
            {
                if (await context.DbSet<User>().Where(x => x.Login == command.UserDto.Login)
                    .AnyAsync(cancellationToken))
                {
                    throw new ArgumentException("User with same login already exits");
                }

                var user = mapper.Map<Data.Models.User>(command.UserDto);
                await context.DbSet<Data.Models.User>().AddAsync(user, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                var userDto = mapper.Map<User>(user);
                return new UserEnvelope(userDto);
            }
        }
    }
}