using System;
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

namespace PrintCenter.Domain.Materials
{
    public class Create
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

        public class MaterialDtoValidator : AbstractValidator<MaterialDto>
        {
            public MaterialDtoValidator()
            {
                RuleFor(material => material.Name).NotNull().NotEmpty();
                RuleFor(material => material.Parameter).NotEqual(0.0);
                RuleFor(material => material.Unit).NotNull().NotEmpty();
                RuleFor(material => material.Price).NotEqual(0.0m);
            }
        }

        public class Command : IRequest<Unit>
        {
            public MaterialDto MaterialDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.MaterialDto).NotNull().SetValidator(new MaterialDtoValidator());
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
                if (await context.Materials.Where(x => x.Name == command.MaterialDto.Name && Math.Abs(x.Parameter - command.MaterialDto.Parameter) < 0.001).AnyAsync(cancellationToken))
                {
                    throw new RestException(HttpStatusCode.BadRequest, $"Material with full name {command.MaterialDto.Name} already exits.");
                }

                var material = mapper.Map<Data.Models.Material>(command.MaterialDto);

                await context.Materials.AddAsync(material, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
