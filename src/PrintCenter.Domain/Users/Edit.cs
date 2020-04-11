using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Users
{
    public class Edit
    {
        public class UserDto
        {
            public string Password { get; set; }

            public string Surname { get; set; }

            public string Name { get; set; }

            public Role? Role { get; set; }

            public List<string> TechnologiesNames { get; set; }
        }

        public class Command : IRequest
        {
            public string Login { get; set; }

            public UserDto UserDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserDto).NotNull();
                RuleFor(x => x.UserDto.Name).NotNull().NotEmpty();
                RuleFor(x => x.UserDto.Surname).NotNull().NotEmpty();
                RuleFor(x => x.UserDto.Role).IsInEnum().WithState(command => command.UserDto.Role <= Role.SuperAdmin);
                RuleFor(x => x.Login).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IPasswordHasher<Data.Models.User> hasher;

            public Handler(DataContext context, IPasswordHasher<Data.Models.User> hasher)
            {
                this.context = context;
                this.hasher = hasher;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await context.Users
                    .Where(x => x.Login.Equals(command.Login))
                    .Include(u => u.UserTechnologies)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, $"Account with login {command.Login} not found.");
                }

                user.Name = command.UserDto.Name ?? user.Name;
                user.Surname = command.UserDto.Surname ?? user.Surname;
                user.Role = command.UserDto.Role ?? user.Role;

                if (!string.IsNullOrWhiteSpace(command.UserDto.Password))
                {
                    user.PasswordHash = hasher.HashPassword(user, command.UserDto.Password);
                }

                user.UserTechnologies.RemoveAll(technology => technology.UserId.Equals(user.Id));

                foreach (var userDtoTechnologiesName in command.UserDto.TechnologiesNames)
                {
                    var technology =
                        await context.Technologies.SingleOrDefaultAsync(_ => _.Name.Equals(userDtoTechnologiesName),
                            cancellationToken);
                    if (technology != null)
                        user.UserTechnologies.Add(new UserTechnology {UserId = user.Id, TechnologyId = technology.Id});
                }

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}