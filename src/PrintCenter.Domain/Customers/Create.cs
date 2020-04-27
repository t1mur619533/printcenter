using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Customers
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            public string Name { get; set; }

            public string Description { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
            }
        }
        
        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IDataContext context;
            private readonly IMapper mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(Command command, CancellationToken cancellationToken)
            {
                if (await context.DbSet<Data.Models.Customer>().Where(x => x.Name == command.Name)
                    .AnyAsync(cancellationToken))
                {
                    throw new DuplicateException<Customer>(command.Name);
                }

                var customer = mapper.Map<Data.Models.Customer>(command);

                await context.DbSet<Data.Models.Customer>().AddAsync(customer, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return customer.Id;
            }
        }
    }
}