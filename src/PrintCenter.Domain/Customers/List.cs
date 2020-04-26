using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;

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
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CustomersEnvelope> Handle(Query query, CancellationToken cancellationToken)
            {
                var customers = await context.Customers
                    .Skip(query.Offset ?? 0)
                    .Take(query.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new CustomersEnvelope(mapper.Map<List<Customer>>(customers), context.Customers.Count());
            }
        }
    }
    
    
}