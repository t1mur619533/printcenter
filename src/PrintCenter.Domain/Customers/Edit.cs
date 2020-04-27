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
    public class Edit
    {
        public class Command : IRequest<int>
        {
            public int Id { get; set; }
            
            public string Name { get; set; }

            public string Description { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
                RuleFor(x => x.Name).Length(1, 255).WithMessage("Имя должно быть не короче 1 символа и не длиннее 255 символов");
                RuleFor(x => x.Description).Length(0, 255).WithMessage("Описание должно быть не длиннее 255 символов");
            }
        }
        
        public class Handler : IRequestHandler<Command, int>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<int> Handle(Command command, CancellationToken cancellationToken)
            {
                var customer = await context.Customers
                    .Where(x => x.Id == command.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (customer == null)
                {
                    throw new NotFoundException<Customer>($"id {command.Id}");
                }
                
                mapper.Map(command, customer);

                await context.SaveChangesAsync(cancellationToken);

                return customer.Id;
            }
        }
    }
}