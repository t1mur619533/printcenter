using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Infrastructure.Accessors;

namespace PrintCenter.Domain.Customers
{
    public class List
    {
        public class Query : IRequest<CustomersEnvelope>
        {
            public Query(int? limit, int? offset)
            {
                Limit = limit;
                Offset = offset;
            }
            public int? Limit { get; }
            public int? Offset { get; }
        }
        
        public class QueryHandler : IRequestHandler<Query, CustomersEnvelope>
        {
            private readonly DataContext context;

            public QueryHandler(DataContext context, IMapper mapper, ICurrentUserAccessor currentUserAccessor)
            {
                this.context = context;
            }

            public async Task<CustomersEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var queryable = context.Customers;

                var customers = await queryable
                    .Skip(message.Offset ?? 0)
                    .Take(message.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new CustomersEnvelope()
                {
                    Customers = customers
                };
            }
        }
    }
    
    
}