using System.Linq;
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
    public class Create
    {
        public class CustomerDto
        {
            public string Name { get; set; }

            public string Description { get; set; }
        }
        
        public class UserDtoValidator : AbstractValidator<CustomerDto>
        {
            public UserDtoValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
            }
        }
        
        public class Command : IRequest
        {
            public CustomerDto CustomerDto { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CustomerDto).NotNull().SetValidator(new UserDtoValidator());
            }
        }
        
        public class Handler : IRequestHandler<Command>
        {
            private readonly IDataContext context;
            private readonly IMapper mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                if (await context.DbSet<Data.Models.Customer>().Where(x => x.Name == command.CustomerDto.Name)
                    .AnyAsync(cancellationToken))
                {
                    throw new DuplicateException<Customer>(command.CustomerDto.Name);
                }

                var customer = mapper.Map<Data.Models.Customer>(command.CustomerDto);

                await context.DbSet<Data.Models.Customer>().AddAsync(customer, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}