using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Materials
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
                var material = await context.Materials.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (material == null)
                {
                    throw new NotFoundException<Material>($"id {command.Id}");
                }

                context.Materials.Remove(material);

                await context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}
