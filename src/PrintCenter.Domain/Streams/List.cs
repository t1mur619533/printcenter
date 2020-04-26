using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Streams
{
    public class List
    {
        public class Query : IRequest<StreamsEnvelope>
        {
            public Query(int? limit, int? offset)
            {
                Limit = limit;
                Offset = offset;
            }

            public int? Limit { get; }
            public int? Offset { get; }
        }

        public class QueryHandler : IRequestHandler<Query, StreamsEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<StreamsEnvelope> Handle(Query query, CancellationToken cancellationToken)
            {
                var streams = await context.Streams
                    .Skip(query.Offset ?? 0)
                    .Take(query.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                var count = context.Streams.AsNoTracking().Count();

                return new StreamsEnvelope(mapper.Map<List<Stream>>(streams), count);
            }
        }
    }
}