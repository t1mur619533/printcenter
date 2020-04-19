using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Domain.Materials
{
    public class Edit
    {
        public class MaterialDto
        {
            public string Name { get; set; }

            public double Parameter { get; set; }

            public string Unit { get; set; }

            public decimal Price { get; set; }

            public string Description { get; set; }

            public double Count { get; set; }

            public double NormalCount { get; set; }

            public double MinimalCount { get; set; }
        }

        public class Command : IRequest<Unit>
        {
            public MaterialDto MaterialDto { get; set; }
            public int Id { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(material => material.MaterialDto).NotNull();
                RuleFor(material => material.MaterialDto.Name).NotNull().NotEmpty();
                RuleFor(material => material.MaterialDto.Parameter).NotEqual(0.0);
                RuleFor(material => material.MaterialDto.Unit).NotNull().NotEmpty();
                RuleFor(material => material.MaterialDto.Price).NotEqual(0.0m);
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
                var material = await context.Materials.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (material == null)
                {
                    throw new NotFoundException<Material>($"id {command.Id}");
                }
                
                mapper.Map(command.MaterialDto, material);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
