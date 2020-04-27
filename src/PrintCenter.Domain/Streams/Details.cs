using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Streams
{
    public class Details
    {
        public class Query : IRequest<Stream>
        {
            public string Code { get; set; }

            public Query(string code)
            {
                Code = code;
            }
        }

        public class QueryHandler : IRequestHandler<Query, Stream>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Stream> Handle(Query query, CancellationToken cancellationToken)
            {
                var stream =
                    await context.Streams.FirstOrDefaultAsync(x => x.Code.Equals(query.Code), cancellationToken);

                if (stream == null)
                {
                    throw new NotFoundException<Stream>($"id {query.Code}");
                }

                return mapper.Map<Stream>(stream);
            }
        }
    }
}