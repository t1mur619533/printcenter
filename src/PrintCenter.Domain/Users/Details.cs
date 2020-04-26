using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Users
{
    public class Details
    {
        public class Query : IRequest<User>
        {
            public string Login { get; set; }

            public Query(string login)
            {
                Login = login;
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, User>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<User> Handle(Query query, CancellationToken cancellationToken)
            {
                var user = await context.Users
                    .Include(u => u.UserTechnologies)
                    .ThenInclude(t => t.Technology)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Login.Equals(query.Login), cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException<User>(query.Login);
                }

                return mapper.Map<User>(user);
            }
        }
    }
}