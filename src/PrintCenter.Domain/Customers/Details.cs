using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Customers
{
    public class Details
    {
        public class Query : IRequest<Customer>
        {
            public int Id { get; set; }
            
            public Query(int id)
            {
                Id = id;
            }
        }
        
        public class QueryHandler : IRequestHandler<Query, Customer>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Customer> Handle(Query message, CancellationToken cancellationToken)
            {
                var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == message.Id, cancellationToken);

                if (customer == null)
                {
                    throw new NotFoundException<Customer>($"id {message.Id}");
                }
                
                return mapper.Map<Customer>(customer);
            }
        }
    }
}