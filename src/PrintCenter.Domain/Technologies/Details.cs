using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Technologies
{
    public class Details
    {
        public class Query : IRequest<Technology>
        {
            public int Id { get; set; }

            public Query(int id)
            {
                Id = id;
            }
        }

        public class QueryHandler : IRequestHandler<Query, Technology>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Technology> Handle(Query query, CancellationToken cancellationToken)
            {
                var technology =
                    await context.Technologies.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

                if (technology == null)
                {
                    throw new NotFoundException<Technology>($"id {query.Id}");
                }

                return mapper.Map<Technology>(technology);
            }
        }
    }
}