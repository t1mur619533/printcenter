using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;
using User = PrintCenter.Shared.User;

namespace PrintCenter.Domain.Users
{
    public class Create
    {
        public class Command : IRequest<UserDetail>
        {
            public User User { get; set; }

            public Command(User user)
            {
                User = user;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).NotNull();
                RuleFor(x => x.User.Login).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.User.Password).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.User.Name).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.User.Role).Must(s => Enum.TryParse<Role>(s, out _)).WithMessage("Invalid role");
                RuleFor(x => x.User.Surname).NotNull().NotEmpty().Length(1, 255);
            }
        }

        public class CommandHandler : IRequestHandler<Command, UserDetail>
        {
            private readonly DataContext context;
            private readonly IPasswordHasher<Data.Models.User> hasher;
            private readonly IMapper mapper;

            public CommandHandler(DataContext context, IMapper mapper, IPasswordHasher<Data.Models.User> hasher)
            {
                this.context = context;
                this.mapper = mapper;
                this.hasher = hasher;
            }

            public async Task<UserDetail> Handle(Command command, CancellationToken cancellationToken)
            {
                var userDto = command.User;
                if (await context.Users.AnyAsync(x => x.Login == userDto.Login, cancellationToken))
                {
                    throw new DuplicateException<User>(userDto.Login);
                }

                var user = mapper.Map<Data.Models.User>(command.User);
                user.PasswordHash = hasher.HashPassword(user, userDto.Password);
                await context.Users.AddAsync(user, cancellationToken);
                
                if (command.User.TechnologyNames != null)
                    foreach (var technologyName in command.User.TechnologyNames)
                    {
                        var technology =
                            await context.Technologies.SingleOrDefaultAsync(_ => _.Name.Equals(technologyName),
                                cancellationToken);
                        if (technology != null)
                            user.UserTechnologies.Add(new UserTechnology
                                {UserId = user.Id, TechnologyId = technology.Id});
                    }

                await context.SaveChangesAsync(cancellationToken);
                return mapper.Map<UserDetail>(user);
            }
        }

        public class CommandPostProcessor : IRequestPostProcessor<Command, UserDetail>
        {
            private readonly DataContext context;

            public CommandPostProcessor(DataContext context)
            {
                this.context = context;
            }

            public async Task Process(Command request, UserDetail response, CancellationToken cancellationToken)
            {
                await context.Tickets.AddAsync(
                    new Data.Models.Ticket
                    {
                        UserId = response.Id,
                        Content = $"Поздравляем, {response.Name} {response.Surname}! " +
                                  $"Вы – новый пользователь системы «Центр печати»! Ваш уровень доступа : {response.Role}. Перед началом работы пройдите инструктаж."
                    }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public class Notification : Infrastructure.Notification<string>
        {
            public UserDetail Subject { get; }

            public Notification(UserDetail user)
            {
                Subject = user;
                Content =
                    $"Поздравляем, {user.Name} {user.Surname}! Вы – новый пользователь системы «Центр печати»! Ваш уровень доступа : {user.Role}. Перед началом работы пройдите инструктаж.";
            }
        }

        public class EmailNotificationHandler : INotificationHandler<Notification>
        {
            public async Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                //отправка письма на email
                await Task.CompletedTask;
            }
        }

        public class PushNotificationHandler : INotificationHandler<Notification>
        {
            public async Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                //отправка пуш уведомления
                await Task.CompletedTask;
            }
        }
    }
}
