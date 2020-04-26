using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;
using PrintCenter.Domain.Exceptions;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Materials
{
    public class Edit
    {
        public class Command : IRequest<Unit>
        {
            public Material Material { get; set; }

            public Command(Material material)
            {
                Material = material;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(material => material).NotNull();
                RuleFor(material => material.Material.Name).NotNull().NotEmpty();
                RuleFor(material => material.Material.Parameter).NotEqual(0.0);
                RuleFor(material => material.Material.Unit).NotNull().NotEmpty();
                RuleFor(material => material.Material.Price).NotEqual(0.0m);
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
                var material =
                    await context.Materials.FirstOrDefaultAsync(x => x.Id.Equals(command.Material.Id),
                        cancellationToken);

                if (material == null)
                {
                    throw new NotFoundException<Material>($"id {command.Material.Id}");
                }

                mapper.Map(command.Material, material);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}