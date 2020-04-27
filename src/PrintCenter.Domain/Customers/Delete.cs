using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Customers
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Command(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
        }
        
        public class QueryHandler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            public QueryHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (customer == null)
                {
                    throw new NotFoundException<Customer>($"id {command.Id}");
                }

                context.Customers.Remove(customer);

                await context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}