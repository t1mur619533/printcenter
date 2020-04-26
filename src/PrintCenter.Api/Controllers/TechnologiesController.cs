using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintCenter.Domain.Technologies;
using PrintCenter.Shared;

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
        public async Task<TechnologiesEnvelope> Get([FromQuery] int limit, [FromQuery] int offset)
        {
            return await mediator.Send(new List.Query(limit, offset));
        }
        
        [HttpGet("{id}")]
        public async Task<Technology> Get(int id)
        {
            return await mediator.Send(new Details.Query(id));
        }

        [HttpPost]
        public async Task Create([FromBody] Technology technology)
        {
            await mediator.Send(new Create.Command(technology));
        }
        
        [HttpPut("{id}")]
        public async Task Edit(int id, [FromBody] Technology technology)
        {
            var command = new Edit.Command(technology) {Technology = {Id = id}};
            await mediator.Send(command);
        }
        
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await mediator.Send(new Delete.Command(id));
        }
    }
}