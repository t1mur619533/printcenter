using System.Collections.Generic;
using System.Linq;
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
        public class Command : IRequest
        {
            public string Login { get; set; }

            public string Password { get; set; }

            public string Surname { get; set; }

            public string Name { get; set; }

            public Role? Role { get; set; }

            public List<string> TechnologiesNames { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Surname).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Role).IsInEnum();
                RuleFor(x => x.Login).NotNull().NotEmpty().Length(1, 255);
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
                    .ThenInclude(ut => ut.Technology)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException<User>(command.Login);
                }

                user.Name = command.Name ?? user.Name;
                user.Surname = command.Surname ?? user.Surname;
                user.Role = command.Role ?? user.Role;

                if (!string.IsNullOrWhiteSpace(command.Password))
                {
                    user.PasswordHash = hasher.HashPassword(user, command.Password);
                }

                var userTechNames = user.UserTechnologies.Select(ut => ut.Technology.Name).ToList();
                var addedTechsNames = command.TechnologiesNames.Except(userTechNames);
                var removedTechNames = userTechNames.Except(command.TechnologiesNames);

                user.UserTechnologies.RemoveAll(technology => removedTechNames.Contains(technology.Technology.Name));

                var addedTechs = await context.Technologies
                    .Where(t => addedTechsNames.Contains(t.Name))
                    .Select(t => t.Id)
                    .ToListAsync(cancellationToken);

                addedTechs.ForEach(technologyId =>
                    user.UserTechnologies.Add(new UserTechnology {UserId = user.Id, TechnologyId = technologyId}));

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}