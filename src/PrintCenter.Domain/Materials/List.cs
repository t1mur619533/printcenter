using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Materials
{
    public class List
    {
        public class Query : IRequest<MaterialsEnvelope>
        {
            public Query(int limit = 100, int offset = 0)
            {
                Limit = limit;
                Offset = offset;
            }

            public int Limit { get; }
            public int Offset { get; }
        }

        public class QueryHandler : IRequestHandler<Query, MaterialsEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<MaterialsEnvelope> Handle(Query query, CancellationToken cancellationToken)
            {
                var materials = await context.Materials
                    .Skip(query.Offset)
                    .Take(query.Limit)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                var count = context.Materials.AsNoTracking().Count();

                return new MaterialsEnvelope(mapper.Map<List<Material>>(materials), count);
            }
        }
    }
}