using System;
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

namespace PrintCenter.Domain.Materials
{
    public class Create
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
                RuleFor(x => x.Material).NotNull();
                RuleFor(x => x.Material.Name).NotNull().NotEmpty();
                RuleFor(x => x.Material.Parameter).NotEqual(0.0);
                RuleFor(x => x.Material.Unit).NotNull().NotEmpty();
                RuleFor(x => x.Material.Price).NotEqual(0.0m);
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
                if (await context.Materials
                    .Where(x => x.Name == command.Material.Name &&
                                Math.Abs(x.Parameter - command.Material.Parameter) < 0.001).AnyAsync(cancellationToken))
                {
                    throw new DuplicateException<Material>(command.Material.Name);
                }

                var material = mapper.Map<Data.Models.Material>(command.Material);
                await context.Materials.AddAsync(material, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}