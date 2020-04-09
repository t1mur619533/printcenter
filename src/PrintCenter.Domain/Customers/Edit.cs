using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Customers
{
    public class Edit
    {
        public class CustomerDto
        {
            public string Name { get; set; }

            public string Description { get; set; }
        }

        public class Command : IRequest<Unit>
        {
            public CustomerDto CustomerDto { get; set; }
            public int Id { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CustomerDto).NotNull();
                RuleFor(x => x.CustomerDto.Name).NotNull().NotEmpty();
            }
        }
        
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var customer = await context.Customers
                    .Where(x => x.Id == command.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (customer == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                
                mapper.Map(command.CustomerDto, customer);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}