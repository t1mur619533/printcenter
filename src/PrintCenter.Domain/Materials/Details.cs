using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Materials
{
    public class Details
    {
        public class Query : IRequest<Material>
        {
            public int Id { get; set; }

            public Query(int id)
            {
                Id = id;
            }
        }

        public class QueryHandler : IRequestHandler<Query, Material>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Material> Handle(Query query, CancellationToken cancellationToken)
            {
                var material =
                    await context.Materials.FirstOrDefaultAsync(x => x.Id.Equals(query.Id), cancellationToken);

                if (material == null)
                {
                    throw new NotFoundException<Material>($"id {query.Id}");
                }

                return mapper.Map<Material>(material);
            }
        }
    }
}