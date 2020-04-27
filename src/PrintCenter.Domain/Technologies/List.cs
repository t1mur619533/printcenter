using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Technologies
{
    public class List
    {
        public class Query : IRequest<TechnologiesEnvelope>
        {
            public Query(int limit, int offset)
            {
                Limit = limit;
                Offset = offset;
            }

            public int Limit { get; }
            public int Offset { get; }
        }

        public class QueryHandler : IRequestHandler<Query, TechnologiesEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<TechnologiesEnvelope> Handle(Query query, CancellationToken cancellationToken)
            {
                var technologies = await context.Technologies
                    .Skip(query.Offset)
                    .Take(query.Limit)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                var count = context.Technologies.AsNoTracking().Count();

                return new TechnologiesEnvelope(mapper.Map<List<Technology>>(technologies), count);
            }
        }
    }
}