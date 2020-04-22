using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;

namespace PrintCenter.Domain.Tickets
{
    public class List
    {
        public class Query : IRequest<TicketsEnvelope>
        {
            public Query(string login, int? offset = 0, int? limit = 20)
            {
                Login = login;
                Limit = limit;
                Offset = offset;
            }

            public string Login { get; }

            public int? Limit { get; }

            public int? Offset { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Login).NotNull().NotEmpty();
                RuleFor(x => x.Limit).LessThanOrEqualTo(1000).GreaterThanOrEqualTo(0);
                RuleFor(x => x.Offset).LessThanOrEqualTo(int.MaxValue).GreaterThanOrEqualTo(0);
            }
        }

        public class QueryHandler : IRequestHandler<Query, TicketsEnvelope>
        {
            private readonly DataContext context;

            public QueryHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<TicketsEnvelope> Handle(Query query, CancellationToken cancellationToken)
            {
                var notifications = await context.Tickets
                    .Include(_ => _.User)
                    .Where(_ => _.User.Login.Equals(query.Login))
                    .Where(_ => _.DelayedDate <= DateTime.Now)
                    .Skip(query.Offset ?? 0)
                    .Take(query.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new TicketsEnvelope(notifications);
            }
        }
    }
}
