using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Technologies
{
    public class Details
    {
        public class Query : IRequest<TechnologyEnvelope>
        {
            public int Id { get; set; }
            
            public Query(int id)
            {
                Id = id;
            }
        }
        
        public class QueryHandler : IRequestHandler<Query, TechnologyEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<TechnologyEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var technology = await context.Technologies.FirstOrDefaultAsync(x => x.Id == message.Id, cancellationToken);

                if (technology == null)
                {
                    throw new NotFoundException<Technology>($"id {message.Id}");
                }
                
                return new TechnologyEnvelope(mapper.Map<Technology>(technology));
            }
        }
    }
}