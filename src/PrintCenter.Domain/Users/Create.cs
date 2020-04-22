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

namespace PrintCenter.Domain.Users
{
    public class Create
    {
        public class Command : IRequest<User>
        {
            public string Login { get; set; }

            public string Password { get; set; }

            public string Surname { get; set; }

            public string Name { get; set; }

            public Role Role { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Password).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Role).IsInEnum();
                RuleFor(x => x.Surname).NotNull().NotEmpty().Length(1, 255);
            }
        }

        public class CommandHandler : IRequestHandler<Command, User>
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

            public async Task<User> Handle(Command command, CancellationToken cancellationToken)
            {
                if (await context.Users.AnyAsync(x => x.Login == command.Login, cancellationToken))
                {
                    throw new DuplicateException<User>(command.Login);
                }

                var user = mapper.Map<Data.Models.User>(command);
                user.PasswordHash = hasher.HashPassword(user, command.Password);
                await context.Users.AddAsync(user, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return mapper.Map<User>(user);
            }
        }

        public class CommandPostProcessor : IRequestPostProcessor<Command, User>
        {
            private readonly DataContext context;

            public CommandPostProcessor(DataContext context)
            {
                this.context = context;
            }

            public async Task Process(Command request, User response, CancellationToken cancellationToken)
            {
                await context.Tickets.AddAsync(
                    new Ticket
                    {
                        UserId = response.Id,
                        Content = $"Поздравляем, {request.Name} {request.Surname}! " +
                                  $"Вы – новый пользователь системы «Центр печати»! Ваш уровень доступа : {request.Role}. Перед началом работы пройдите инструктаж."
                    }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public class Notification : Infrastructure.Notification<string>
        {
            public User Subject { get; }

            public Notification(User user)
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
