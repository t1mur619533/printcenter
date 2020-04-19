using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Customers
{
    public class Details
    {
        public class Query : IRequest<CustomerEnvelope>
        {
            public int Id { get; set; }
            
            public Query(int id)
            {
                Id = id;
            }
        }
        
        public class QueryHandler : IRequestHandler<Query, CustomerEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CustomerEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == message.Id, cancellationToken);

                if (customer == null)
                {
                    throw new NotFoundException<Customer>($"id {message.Id}");
                }
                
                return new CustomerEnvelope(mapper.Map<Customer>(customer));
            }
        }
    }
}