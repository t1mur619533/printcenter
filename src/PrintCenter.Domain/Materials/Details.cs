using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Materials
{
    public class Details
    {
        public class Query : IRequest<MaterialEnvelope>
        {
            public int Id { get; set; }
            
            public Query(int id)
            {
                Id = id;
            }
        }
        
        public class QueryHandler : IRequestHandler<Query, MaterialEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<MaterialEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var material = await context.Materials.FirstOrDefaultAsync(x => x.Id == message.Id, cancellationToken);

                if (material == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                
                return new MaterialEnvelope(mapper.Map<Material>(material));
            }
        }
    }
}
