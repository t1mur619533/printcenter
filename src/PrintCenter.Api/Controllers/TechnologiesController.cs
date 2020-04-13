using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintCenter.Domain.Technologies;

namespace PrintCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public TechnologiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<TechnologiesEnvelope> Get([FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await mediator.Send(new List.Query(limit, offset));
        }
        
        [HttpGet("{id}")]
        public async Task<TechnologyEnvelope> Get(int id)
        {
            return await mediator.Send(new Details.Query(id));
        }

        [HttpPost]
        public async Task Create([FromBody]Create.Command command)
        {
            await mediator.Send(command);
        }
        
        [HttpPut("{id}")]
        public async Task Edit(int id, [FromBody]Edit.Command command)
        {
            command.Id = id;
            await mediator.Send(command);
        }
        
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await mediator.Send(new Delete.Command(id));
        }
    }
}