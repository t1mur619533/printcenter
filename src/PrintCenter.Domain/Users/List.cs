using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Users
{
    public class List
    {
        public class Query : IRequest<UsersEnvelope>
        {
            public Query(int limit, int offset)
            {
                Limit = limit;
                Offset = offset;
            }

            public int? Limit { get; }

            public int? Offset { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Limit).LessThanOrEqualTo(1000).GreaterThanOrEqualTo(0);
                RuleFor(x => x.Offset).LessThanOrEqualTo(int.MaxValue).GreaterThanOrEqualTo(0);
            }
        }

        public class QueryHandler : IRequestHandler<Query, UsersEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<UsersEnvelope> Handle(Query query, CancellationToken cancellationToken)
            {
                var users = await context.Users
                    .Include(user => user.UserTechnologies)
                    .ThenInclude(technology => technology.Technology)
                    .Skip(query.Offset ?? 0)
                    .Take(query.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                var count = context.Tickets.AsNoTracking().Count();

                return new UsersEnvelope(mapper.Map<List<UserDetail>>(users), count);
            }
        }
    }
    
    
}