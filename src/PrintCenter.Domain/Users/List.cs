using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;

namespace PrintCenter.Domain.Users
{
    public class List
    {
        public class Query : IRequest<UsersEnvelope>
        {
            public Query(int? limit, int? offset)
            {
                Limit = limit;
                Offset = offset;
            }
            public int? Limit { get; }
            public int? Offset { get; }
        }
        
        public class QueryHandler : IRequestHandler<Query, UsersEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<UsersEnvelope> Handle(Query query, CancellationToken cancellationToken)
            {
                var users = await context.Users
                    .Include(user => user.UserTechnologies)
                    .ThenInclude(technology => technology.Technology)
                    .Skip(query.Offset ?? 0)
                    .Take(query.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new UsersEnvelope(mapper.Map<List<UserEnvelope>>(users));
            }
        }
    }
    
    
}